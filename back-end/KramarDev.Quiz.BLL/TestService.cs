using DAL = KramarDev.Quiz.DALAbstractions.Dto;
using BL = KramarDev.Quiz.BLLAbstractions.Dto;

namespace KramarDev.Quiz.BLL;

public class TestService(IUnitOfWork uow, IAppCacheService cacheService) : ITestService
{
    private readonly IUnitOfWork _uow = uow;
    private readonly IAppCacheService _cache = cacheService;

    public async Task<BL.TestDto> CreateTestAsync(string technologyName, string userName, string ipAddress)
    {
        BL.TechnologyDto technology = await _cache.GetTechnologyByNameAsync(technologyName);
        NewTestData testData = await GenerateRandomQuestionsForTestAsync(technology);

        DAL.NewTestDto newTest = new DAL.NewTestDto
        {
            TechnologyId = testData.TechnologyId,
            Username = userName,
            StartDate = DateTime.UtcNow,
            RemoteIpAddress = ipAddress,
            QuestionIds = testData.QuestionIds,
        };

        int testId = await _uow.TestRepository.CreateTestAsync(newTest);
        BL.QuestionDto firstQuestion = await GetNextQuestionAsync(testId, userName);

        return new BL.TestDto
        {
            TestId = testId,
            TechnologyName = technologyName,
            QuestionCount = testData.Amount,
            SecondsLeft = technology.DurationInMinutes * 60,
            Question = firstQuestion,
        };
    }

    public async Task AnswerAsync(int testId, int questionId, byte answerNumber, string username)
    {
        bool canAnswer = await _uow.TestRepository.CanAnswerQuestionAsync(testId, questionId, username);
        if (!canAnswer)
        {
            throw new InvalidOperationException(
                $"User {username} cannot answer question {questionId} for test {testId}");
        }

        DAL.QuestionInfoDto questionInfo = await _uow.TestRepository.GetQuestionInfoAsync(questionId);

        DAL.QuestionAnswerDto answer = new()
        {
            TestId = testId,
            QuestionId = questionId,
            AnswerNumber = answerNumber,
            AnswerPoints = (byte)(answerNumber == questionInfo.CorrectAnswerNumber ? 1 : 0),
            AnswerDate = DateTime.UtcNow
        };

        await _uow.TestRepository.AnswerAndSaveAsync(answer);
    }

    public async Task<BL.QuestionDto> GetNextQuestionAsync(int testId, string userName)
    {
        DAL.QuestionDto nextQuestionDAL =
            await _uow.ComplexQueriesRepository.SelectNextQuestionAsync(testId, userName);

        if (nextQuestionDAL != null)
        {
            Task taskUpdate = _uow.TestQuestionRepository.UpdateTestQuestionDateAsync(
                testId, nextQuestionDAL.TestQuestionId);

            BL.QuestionDto nextQuestion = DtoMapper.FromDAL(nextQuestionDAL);

            await taskUpdate;

            await _uow.SaveAsync();

            return nextQuestion;
        }

        return null;
    }

    public async Task<AnswerResponseDto> AnswerAndNextAsync(int testId, int questionId, byte answerNumber, string userName)
    {
        AnswerResponseDto responseDto = new();

        await AnswerAsync(testId, questionId, answerNumber, userName);
        responseDto.NextQuestion = await GetNextQuestionAsync(testId, userName);

        /* 
         * All questions have been answered.
         * Complete the test and retrieve the final results.
         */
        if (responseDto.NextQuestion == null)
        {
            AnswerDto[] answers = DtoMapper.FromDAL(
                await _uow.QuestionRepository.GetAnswersAsync(testId, userName));

            responseDto.TestResult = new TestResultDto { Answers = answers };

            CalculateFinalScore(answers,
                out float finalScore, out int totalPoints, out int earnedPoints, out int answeredCount);

            DAL.TechnologyDto technology = await _uow.TechnologyRepository.GetTechnologyByTestIdAsync(testId);

            responseDto.TestResult.TotalPoints = totalPoints;
            responseDto.TestResult.FinalScore = finalScore;
            responseDto.TestResult.EarnedPoints = earnedPoints;
            responseDto.TestResult.AnsweredCount = answeredCount;
            responseDto.TestResult.TechnologyName = technology.Name;

            await _uow.TestRepository.CompleteTestAndSaveAsync(
                userName, testId, finalScore);
        }

        return responseDto;
    }

    public async Task<BL.TestDto> RestoreCurrentTestAsync(string userName)
    {
        int? testId = await _uow.TestRepository.GetActiveTestByUserAsync(userName);
        if (testId == null)
        {
            return null;
        }

        DAL.CurrentTestStateDto dalTestDto = await _uow.TestRepository.GetCurrentTestStateAsync(testId.Value);
        if (dalTestDto == null)
        {
            return null;
        }

        BL.TechnologyDto technology = await _cache.GetTechnologyByIdAsync(dalTestDto.TechnologyId);
        int secondsLeft = technology.DurationInMinutes * 60 - dalTestDto.SpentTimeInSeconds;

        BL.TestDto testDto = new()
        {
            SecondsLeft = secondsLeft,
            TestId = dalTestDto.TestId,
            QuestionCount = dalTestDto.TotalQuestions,
            Question = DtoMapper.FromDAL(dalTestDto.CurrentQuestion),
            TechnologyName = technology.Name,
        };

        return testDto;
    }

    public async Task CancelTestAsync(string userName, int testId)
    {
        await _uow.TestRepository.CancelTestAsync(userName, testId);
    }

    private async Task<NewTestData> GenerateRandomQuestionsForTestAsync(BL.TechnologyDto technology)
    {
        NewTestData data = new NewTestData();

        data.QuestionIds = await GenerateTestQuestions(technology.Name, technology.QuestionCount);
        data.Amount = technology.QuestionCount;
        data.TechnologyId = technology.Id;

        return data;
    }

    private async Task<int[]> GenerateTestQuestions(string technologyName, int questionAmount)
    {
        int[] easyIds = await _cache.GetEasyQuestionIdsAsync(technologyName);
        int[] middleIds = await _cache.GetMiddleQuestionIdsAsync(technologyName);
        int[] hardIds = await _cache.GetHardQuestionIdsAsync(technologyName);

        int easyAmount = questionAmount / 2;
        int middleAmount = questionAmount / 3;
        int hardAmount = questionAmount - (easyAmount + middleAmount);

        if (easyIds.Length < easyAmount)
        {
            throw new InvalidOperationException(
                $"Not enough easy questions for technology: {technologyName}. " +
                $"Required: {easyAmount}, available: {easyIds.Length}");
        }

        if (middleIds.Length < middleAmount)
        {
            throw new InvalidOperationException(
                $"Not enough middle questions for technology: {technologyName}. " +
                $"Required: {middleAmount}, available: {middleIds.Length}");
        }

        if (hardIds.Length < hardAmount)
        {
            throw new InvalidOperationException(
                $"Not enough hard questions for technology: {technologyName}. " +
                $"Required: {hardAmount}, available: {hardIds.Length}");
        }

        List<int> generatedIds = new List<int>(questionAmount);

        generatedIds.AddRange(Generate(easyIds, easyAmount));
        generatedIds.AddRange(Generate(middleIds, middleAmount));
        generatedIds.AddRange(Generate(hardIds, hardAmount));

        System.Diagnostics.Debug.Assert(generatedIds.Count == questionAmount);

        return generatedIds.ToArray();
    }

    private static int[] Generate(int[] ids, int amount)
    {
        int[] generatedIds = new int[amount];
        var selectedIndices = new HashSet<int>(amount);
        for (int i = 0, index; i < amount; ++i)
        {
            while (!selectedIndices.Add(index = Random.Shared.Next(ids.Length))) ;

            generatedIds[i] = ids[index];
        }

        return generatedIds;
    }

    private static void CalculateFinalScore(AnswerDto[] answers, out float finalScore, out int totalPoints, out int earnedPoints, out int answeredCount)
    {
        totalPoints = 0;
        earnedPoints = 0;
        answeredCount = 0;

        for (int i = 0; i < answers.Length; ++i)
        {
            AnswerDto answer = answers[i];
            totalPoints += answer.Complexity;
            if (answer.Answer == answer.CorrectAnswer)
            {
                earnedPoints += answer.Complexity;
                ++answeredCount;
            }
        }

        finalScore = (earnedPoints / (float)totalPoints) * 100;
    }
}

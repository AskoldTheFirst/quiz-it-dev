using DAL = KramarDev.Quiz.DALAbstractions.Dto;
using BL = KramarDev.Quiz.BLLAbstractions.Dto;

namespace KramarDev.Quiz.BLL.Services;

public class TestService(IUnitOfWork uow, IAppCacheService cacheService) : ITestService
{
    private readonly IUnitOfWork _uow = uow;
    private readonly IAppCacheService _cache = cacheService;


    public async Task<BL.TestDto> CreateTestAsync(string topicName, string userName, string ipAddress)
    {
        BL.TopicDto topic = await _cache.GetTopicByNameAsync(topicName);
        NewTestData testData = await GenerateRandomQuestionsForTestAsync(topic);

        DAL.NewTestDto newTest = new()
        {
            TopicId = testData.TopicId,
            Username = userName,
            StartDate = DateTime.UtcNow,
            RemoteIpAddress = ipAddress,
            QuestionIds = testData.QuestionIds,
            TotalPoints = testData.TotalPoints,
        };

        int testId = await _uow.TestRepository.CreateTestAsync(newTest);
        BL.QuestionDto firstQuestion = await GetNextQuestionAsync(testId, userName);

        return new BL.TestDto
        {
            TestId = testId,
            TopicName = topicName,
            QuestionCount = testData.QuestionIds.Length,
            SecondsLeft = topic.DurationInMinutes * 60,
            Question = firstQuestion,
            TopicColor = topic.ThemeColor,
        };
    }

    public async Task<AnswerResponseDto> AnswerAndNextAsync(
        int testId, int questionId, byte answerNumber, string userName)
    {
        await AnswerAsync(testId, questionId, answerNumber, userName);

        var nextQuestion = await GetNextQuestionAsync(testId, userName);

        // If there is a next question, return it immediately
        if (nextQuestion != null)
        {
            return new AnswerResponseDto
            {
                NextQuestion = nextQuestion,
                TestResult = null
            };
        }

        // All questions answered - build final test result
        AnswerDto[] answers = DtoMapper.FromDAL(
            await _uow.QuestionRepository.GetAnswersAsync(testId, userName));

        CalculateFinalScore(answers,
            out int finalScore, out int finalWeightedScore, out int totalPoints, out int earnedPoints, out int answeredCount);

        DAL.TopicDto topic = await _uow.TopicRepository.GetTopicByTestIdAsync(testId);

        var testResult = new TestResultDto
        {
            Answers = answers,
            TotalPoints = totalPoints,
            FinalScore = finalScore,
            EarnedPoints = earnedPoints,
            AnsweredCount = answeredCount,
            TopicName = topic.Name
        };

        await _uow.TestRepository.CompleteTestAndSaveAsync(
            userName, testId, finalScore, finalWeightedScore, answeredCount, earnedPoints);

        return new AnswerResponseDto
        {
            NextQuestion = null,
            TestResult = testResult
        };
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

        BL.TopicDto topic = await _cache.GetTopicByIdAsync(dalTestDto.TopicId);
        int secondsLeft = topic.DurationInMinutes * 60 - dalTestDto.SpentTimeInSeconds;

        BL.TestDto testDto = new()
        {
            SecondsLeft = secondsLeft,
            TestId = dalTestDto.TestId,
            QuestionCount = dalTestDto.TotalQuestions,
            Question = DtoMapper.FromDAL(dalTestDto.CurrentQuestion),
            TopicName = topic.Name,
            TopicColor = topic.ThemeColor,
        };

        return testDto;
    }

    public async Task CancelTestAsync(string userName, int testId)
    {
        await _uow.TestRepository.CancelTestAsync(userName, testId);
    }

    public async Task<TestResultDto> CompleteAsync(int testId, string userName)
    {
        AnswerDto[] answers = DtoMapper.FromDAL(
                await _uow.QuestionRepository.GetAnswersAsync(testId, userName));

        CalculateFinalScore(answers,
                out int finalScore, out int finalWeightedScore, out int totalPoints, out int earnedPoints, out int answeredCount);

        DAL.TopicDto topic = await _uow.TopicRepository.GetTopicByTestIdAsync(testId);

        var dto = new TestResultDto
        {
            Answers = answers,
            TotalPoints = totalPoints,
            FinalScore = finalScore,
            EarnedPoints = earnedPoints,
            AnsweredCount = answeredCount,
            TopicName = topic.Name
        };

        await _uow.TestRepository.CompleteTestAndSaveAsync(
                userName, testId, finalScore, finalWeightedScore, answeredCount, earnedPoints);

        return dto;
    }


    #region [Private methods]

    private async Task<BL.QuestionDto> GetNextQuestionAsync(int testId, string userName)
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

    private async Task AnswerAsync(int testId, int questionId, byte answerNumber, string username)
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

    private async Task<NewTestData> GenerateRandomQuestionsForTestAsync(BL.TopicDto topic)
    {
        NewTestData data = new();

        var (ids, totalPoints) = await GenerateTestQuestions(
            topic.Name, topic.QuestionCount);

        data = new NewTestData
        {
            QuestionIds = ids,
            TotalPoints = totalPoints,
            TopicId = topic.Id
        };

        return data;
    }

    private async Task<(int[], int)> GenerateTestQuestions(string topicName, int questionAmount)
    {
        int[] easyIds = await _cache.GetEasyQuestionIdsAsync(topicName);
        int[] middleIds = await _cache.GetMiddleQuestionIdsAsync(topicName);
        int[] hardIds = await _cache.GetHardQuestionIdsAsync(topicName);

        int easyAmount = questionAmount / 2;
        int middleAmount = questionAmount / 3;
        int hardAmount = questionAmount - (easyAmount + middleAmount);

        if (easyIds.Length < easyAmount)
        {
            throw new InvalidOperationException(
                $"Not enough easy questions for topic: {topicName}. " +
                $"Required: {easyAmount}, available: {easyIds.Length}");
        }

        if (middleIds.Length < middleAmount)
        {
            throw new InvalidOperationException(
                $"Not enough middle questions for topic: {topicName}. " +
                $"Required: {middleAmount}, available: {middleIds.Length}");
        }

        if (hardIds.Length < hardAmount)
        {
            throw new InvalidOperationException(
                $"Not enough hard questions for topic: {topicName}. " +
                $"Required: {hardAmount}, available: {hardIds.Length}");
        }

        List<int> generatedIds = new List<int>(questionAmount);

        generatedIds.AddRange(Generate(easyIds, easyAmount));
        generatedIds.AddRange(Generate(middleIds, middleAmount));
        generatedIds.AddRange(Generate(hardIds, hardAmount));

        int totalPoints = easyAmount + middleAmount * 2 + hardAmount * 3;

        return (generatedIds.ToArray(), totalPoints);
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

    private static void CalculateFinalScore(AnswerDto[] answers, out int finalScore, out int finalWeightedScore, out int totalPoints, out int earnedPoints, out int answeredCount)
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

        finalScore = CalculateScore(answeredCount, answers.Length);
        finalWeightedScore = CalculateScore(earnedPoints, totalPoints);
    }

    private static int CalculateScore(int earned, int total)
    {
        return (int)Math.Round((earned / (double)total) * 100, MidpointRounding.AwayFromZero);
    }

    #endregion
}

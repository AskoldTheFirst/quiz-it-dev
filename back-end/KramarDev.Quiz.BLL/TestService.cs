using DAL = KramarDev.Quiz.DALAbstractions.Dto;
using BL = KramarDev.Quiz.BLLAbstractions.Dto;
using System.ComponentModel;
using KramarDev.Quiz.DALAbstractions.Dto;

namespace KramarDev.Quiz.BLL;

public class TestService(IUnitOfWork uow, IAppCacheService cacheService) : ITestService
{
    private readonly IUnitOfWork _uow = uow;
    private readonly IAppCacheService _cache = cacheService;

    public async Task<TestCreatedDto> CreateTestAsync(string technologyName, string userName, string ipAddress)
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

        return new TestCreatedDto
        {
            TestId = testId,
            TechnologyName = technologyName,
            TestColor = technology.Color,
            QuestionsAmount = testData.Amount,
            TestDurationInSeconds = technology.DurationInMinutes * 60,
            FirstQuestion = firstQuestion,
        };
    }

    public async Task AnswerAsync(int testId, int questionId, byte answerNumber, string username)
    {
        Task<bool> canAnswer = _uow.TestRepository.CanAnswerQuestionAsync(testId, questionId, username);
        Task<DAL.QuestionInfoDto> questionInfo = _uow.TestRepository.GetQuestionInfoAsync(questionId);

        bool canAnswerQuestion = await canAnswer;

        if (!canAnswerQuestion)
        {
            throw new InvalidOperationException(
                $"User {username} cannot answer question {questionId} for test {testId}");
        }

        DAL.QuestionInfoDto info = await questionInfo;

        DAL.QuestionAnswerDto answer = new()
        {
            TestId = testId,
            QuestionId = questionId,
            AnswerNumber = answerNumber,
            AnswerPoints = (byte)(answerNumber == info.CorrectAnswerNumber ? 1 : 0),
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

            return nextQuestion;
        }

        return null;
    }

    public async Task<NextQuestionStateDto> GetNextQuestionStateAsync(string userName, int? testId)
    {
        TestDto test = await _uow.TestRepository.GetTestByIdAsync(userName, testId);

        BL.QuestionDto nextQuestion = await GetNextQuestionAsync(test.Id, userName);

        BL.TechnologyDto testTechnology = await _cache.GetTechnologyByIdAsync(test.TechnologyId);

        NextQuestionStateDto stateDto = new NextQuestionStateDto
        {
            TechnologyName = testTechnology.Name,
            Question = nextQuestion,
            TotalAmount = testTechnology.QuestionCount,
            SecondsLeft = (int)(test.StartDate.AddMinutes(testTechnology.DurationInMinutes) - DateTime.UtcNow).TotalSeconds,
            QuestionNumber = await _uow.TestQuestionRepository.GetAmountOfAlreadyAnsweredQuestionsAsync(test.Id) + 1
        };

        return stateDto;
    }

    public async Task<BL.TestResultDto> GetTestResultAsync(string userName, int testId)
    {
        Task<int> questionAmount =
            _uow.TestQuestionRepository.GetAmountOfAlreadyAnsweredQuestionsAsync(testId);

        DAL.TestResultDto resultDto =
            await _uow.TestRepository.GetTestResultAsync(userName, testId);

        BL.TestResultDto result = new()
        {
            Score = resultDto.Score,
            TimeSpentInSeconds = resultDto.TimeSpentInSeconds,
            QuestionsAmount = await questionAmount,
        };

        return result;
    }

    public async Task CompleteTestAsync(string userName, int testId)
    {

    }

    public async Task<CurrentTestDto> RestoreCurrentTestAsync(string userName)
    {
        int? testId = await _uow.TestRepository.GetActiveTestByUserAsync(userName);
        if (testId == null)
        {
            return null;
        }

        DAL.CurrentTestStateDto dalTestDto = await _uow.TestRepository.GetCurrentTestStateAsync(testId.Value);
        BL.TechnologyDto technology = await _cache.GetTechnologyByIdAsync(dalTestDto.TechnologyId);

        BL.CurrentTestDto testDto = new()
        {
            Number = dalTestDto.Number,
            SpentTimeInSeconds = dalTestDto.SpentTimeInSeconds,
            TestId = dalTestDto.TestId,
            TotalQuestions = dalTestDto.TotalQuestions,
            Question = DtoMapper.FromDAL(dalTestDto.CurrentQuestion),
            TestName = technology.Name,
            TestColor = technology.Color,
        };

        return testDto;
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
}

namespace KramarDev.Quiz.BLLAbstractions.Interfaces;

public interface ITestService
{
    Task<TestCreatedDto> CreateTestAsync(string technologyName, string userName, string ipAddress);

    Task AnswerAsync(int testId, int questionId, byte answerNumber, string username);

    Task<QuestionDto> GetNextQuestionAsync(int testId, string userName);

    Task<NextQuestionStateDto> GetNextQuestionStateAsync(string userName, int? testId);

    Task<TestResultDto> GetTestResultAsync(string userName, int testId);

    Task CompleteTestAsync(string userName, int testId);

    Task<CurrentTestDto> RestoreCurrentTestAsync(string userName);
}

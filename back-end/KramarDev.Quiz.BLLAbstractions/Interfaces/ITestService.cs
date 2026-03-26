namespace KramarDev.Quiz.BLLAbstractions.Interfaces;

public interface ITestService
{
    Task<TestDto> CreateTestAsync(string technologyName, string userName, string ipAddress);

    Task AnswerAsync(int testId, int questionId, byte answerNumber, string username);

    Task<QuestionDto> GetNextQuestionAsync(int testId, string userName);

    Task<AnswerResponseDto> AnswerAndNextAsync(int testId, int questionId, byte answerNumber, string username);

    Task<TestDto> RestoreCurrentTestAsync(string userName);

    Task CancelTestAsync(string userName, int testId);
}

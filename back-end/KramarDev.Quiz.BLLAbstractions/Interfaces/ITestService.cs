namespace KramarDev.Quiz.BLLAbstractions.Interfaces;

public interface ITestService
{
    Task<TestDto> CreateTestAsync(string topicName, string userName, string ipAddress);

    Task<AnswerResponseDto> AnswerAndNextAsync(
        int testId, int questionId, byte answerNumber, string username);

    Task<TestDto> RestoreCurrentTestAsync(string userName);

    Task CancelTestAsync(string userName, int testId);

    Task<TestResultDto> CompleteAsync(int testId, string userName);
}

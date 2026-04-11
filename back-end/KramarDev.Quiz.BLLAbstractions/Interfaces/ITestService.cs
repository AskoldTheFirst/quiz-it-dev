namespace KramarDev.Quiz.BLLAbstractions.Interfaces;

public interface ITestService
{
    Task<TestDto> CreateTestAsync(string topicName, string userName, string ipAddress, CancellationToken cancellationToken = default);

    Task<AnswerResponseDto> AnswerAndNextAsync(
        int testId, int questionId, byte answerNumber, string username);

    Task<TestDto> RestoreCurrentTestAsync(string userName, CancellationToken cancellationToken = default);

    Task CancelTestAsync(string userName, int testId);

    Task<TestResultDto> CompleteAsync(int testId, string userName);

    Task HideAsync(string userName, CancellationToken cancellationToken);
}

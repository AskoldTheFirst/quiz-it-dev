namespace KramarDev.Quiz.DALAbstractions.Repositories;

public interface ITestRepository
{
    Task<int> CreateTestAsync(NewTestDto newTest);

    Task<QuestionInfoDto> GetQuestionInfoAsync(int questionId);

    Task AnswerAndSaveAsync(QuestionAnswerDto answer);

    Task CompleteTestAndSaveAsync(string userName, int testId, float finalScore);

    Task<int?> GetActiveTestByUserAsync(string userName);

    Task<CurrentTestStateDto> GetCurrentTestStateAsync(int testId);

    Task<bool> CanAnswerQuestionAsync(int testId, int questionId, string userName);

    Task CancelTestAsync(string userName, int testId);
}

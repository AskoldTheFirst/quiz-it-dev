namespace KramarDev.Quiz.DALAbstractions.Repositories;

public interface IComplexQueriesRepository
{
    Task<QuestionDto> SelectNextQuestionAsync(int testId, string userName, CancellationToken cancellationToken = default);
}

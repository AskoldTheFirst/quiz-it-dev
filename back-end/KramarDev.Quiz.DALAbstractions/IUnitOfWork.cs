namespace KramarDev.Quiz.DALAbstractions;

public interface IUnitOfWork
{
    ITestRepository TestRepository { get; }

    IQuestionRepository QuestionRepository { get; }

    ITechnologyRepository TechnologyRepository { get; }

    ITestQuestionRepository TestQuestionRepository { get; }

    IComplexQueriesRepository ComplexQueriesRepository { get; }

    Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();

    Task<int> SaveAsync();

    Task UpdateDbAsync();
}

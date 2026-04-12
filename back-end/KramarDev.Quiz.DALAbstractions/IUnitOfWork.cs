using KramarDev.Quiz.DALAbstractions.Interfaces;

namespace KramarDev.Quiz.DALAbstractions;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    ITestRepository TestRepository { get; }

    IQuestionRepository QuestionRepository { get; }

    ITopicRepository TopicRepository { get; }

    IStatisticsRepository StatisticsRepository { get; }

    Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();

    Task<int> SaveAsync();

    Task UpdateDbAsync();
}

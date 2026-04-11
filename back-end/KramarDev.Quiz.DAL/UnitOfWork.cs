using KramarDev.Quiz.DAL.Repositories;
using KramarDev.Quiz.DALAbstractions;
using KramarDev.Quiz.DALAbstractions.Interfaces;
using System.Data;

namespace KramarDev.Quiz.DAL;

public sealed class UnitOfWork : IUnitOfWork
{
    private static readonly DbContextOptions<QuizDbContext> _options;

    private readonly QuizDbContext _ctx;

    public UnitOfWork(QuizDbContext ctx)
    {
        _ctx = ctx;
    }

    public ITestRepository TestRepository => new TestRepository(_ctx);

    public IQuestionRepository QuestionRepository => new QuestionRepository(_ctx);

    public ITopicRepository TopicRepository => new TopicRepository(_ctx);

    public ITestQuestionRepository TestQuestionRepository => new TestQuestionRepository(_ctx);

    public IComplexQueriesRepository ComplexQueriesRepository => new ComplexQueriesRepository(_ctx);

    public IStatisticsRepository StatisticsRepository => new StatisticsRepository(_ctx);

    public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        await _ctx.Database.BeginTransactionAsync(isolationLevel);
    }

    public async Task CommitTransactionAsync()
    {
        await _ctx.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _ctx.Database.RollbackTransactionAsync();
    }

    public async Task<int> SaveAsync()
    {
        return await _ctx.SaveChangesAsync();
    }

    public async Task UpdateDbAsync()
    {
        await _ctx.Database.MigrateAsync();
        DbInitializer.Initialize(_ctx);
    }
}

using KramarDev.Quiz.DALAbstractions.Interfaces;

namespace KramarDev.Quiz.DAL.Repositories;

public class StatisticsRepository(QuizDbContext dbCtx) : IStatisticsRepository
{
    private readonly QuizDbContext Ctx = dbCtx;

    public async Task<RowDto[]> SelectByFilterAsync(int topicId,
        int scoreThreshold, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        return await (from t in Ctx.Tests
                      join te in Ctx.Topics
                        on t.TopicId equals te.Id
                      orderby t.FinalScore descending
                      where (topicId == 0 || t.TopicId == topicId) &&
                              t.State == TestState.Completed &&
                              !t.IsHidden &&
                              t.FinalScore >= scoreThreshold
                      select new RowDto
                      {
                          User = t.Username,
                          TopicName = te.Name,
                          TopicThemeColor = te.ThemeColor,
                          QuestionTotal = t.QuestionCount,
                          AnsweredCount = t.AnsweredCount,
                          FinalScore = t.FinalScore,
                          FinalWeightedScore = t.FinalWeightedScore,
                          Date = t.FinishDate.Value
                      })
                      .Skip(pageSize * pageNumber)
                      .Take(pageSize)
                      .ToArrayAsync(cancellationToken);
    }

    public async Task<int> GetTotalCountAsync(int topicId,
        int scoreThreshold, CancellationToken cancellationToken = default)
    {
        return await (from t in Ctx.Tests
                      where (topicId == 0 || t.TopicId == topicId) &&
                            t.State == TestState.Completed &&
                            !t.IsHidden &&
                            t.FinalScore >= scoreThreshold
                      select t).CountAsync(cancellationToken);
    }

    public async Task<ProfileDto> GetProfileAsync(string userName, CancellationToken cancellationToken = default)
    {
        // Summary
        var completedTests = await (from t in Ctx.Tests
                                    where t.Username == userName &&
                                            t.State == TestState.Completed &&
                                            !t.IsHidden
                                    select t).ToArrayAsync(cancellationToken);

        var summary = new ProfileSummaryDto
        {
            TotalAttemptCount = completedTests.Length,
            AverageScore = completedTests.Length > 0 ? (int)completedTests.Average(t => t.FinalScore) : 0,
            BestScore = completedTests.Length > 0 ? (int)completedTests.Max(t => t.FinalScore) : 0,
            AnswerCount = completedTests.Sum(t => t.AnsweredCount)
        };

        // Performance by topic
        var topics = await (from t in Ctx.Tests
                            join te in Ctx.Topics on t.TopicId equals te.Id
                            where t.Username == userName && t.State == TestState.Completed
                            group t by new { te.Name, te.ThemeColor } into grp
                            select new PerformanceByTopicDto
                            {
                                Topic = grp.Key.Name,
                                Color = grp.Key.ThemeColor,
                                AttemptCount = grp.Count(),
                                Best = grp.Max(g => g.FinalScore),
                                Average = (int)grp.Average(g => g.FinalScore)
                            }).ToArrayAsync(cancellationToken);

        // Attempts
        var attempts = await (from t in Ctx.Tests
                              join te in Ctx.Topics on t.TopicId equals te.Id
                              where t.Username == userName && t.State == TestState.Completed
                              orderby t.FinishDate descending
                              select new AttemptDto
                              {
                                  Topic = te.Name,
                                  Date = t.FinishDate.Value,
                                  AnsweredCount = t.AnsweredCount,
                                  QuestionCount = t.QuestionCount,
                                  Score = t.FinalScore
                              }).Take(3).ToArrayAsync(cancellationToken);

        return new ProfileDto
        {
            Summary = summary,
            Topics = topics,
            Attempts = attempts
        };
    }

    public async Task HideTestsForUserAndSaveAsync(string userName, CancellationToken cancellationToken)
    {
        await Ctx.Tests
            .Where(t => !t.IsHidden && t.Username == userName)
            .ExecuteUpdateAsync(setters => setters.SetProperty(t => t.IsHidden, true), cancellationToken);
    }
}

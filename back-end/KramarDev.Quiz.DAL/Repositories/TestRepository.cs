using System.Security.Cryptography;

namespace KramarDev.Quiz.DAL.Repositories;

public class TestRepository(QuizDbContext dbCtx) : ITestRepository
{
    private readonly QuizDbContext Ctx = dbCtx;

    public async Task<int> CreateTestAsync(NewTestDto newTest)
    {
        int questionAmount = newTest.QuestionIds.Length;

        Test test = new Test
        {
            TechnologyId = newTest.TechnologyId,
            Username = newTest.Username,
            StartDate = newTest.StartDate,
            IpAddress = newTest.RemoteIpAddress,
        };

        Ctx.Tests.Add(test);

        await Ctx.SaveChangesAsync();

        TestQuestion[] generatedQuestions = new TestQuestion[questionAmount];
        for (int i = 0; i < questionAmount; ++i)
        {
            generatedQuestions[i] = new TestQuestion()
            {
                QuestionId = newTest.QuestionIds[i],
                TestId = test.Id
            };
        }

        Ctx.TestQuestions.AddRange(generatedQuestions);
        await Ctx.SaveChangesAsync();

        return test.Id;
    }

    public async Task<QuestionInfoDto> GetQuestionInfoAsync(int questionId)
    {
        return await (from q in Ctx.Questions
                      where q.Id == questionId
                      select new QuestionInfoDto
                      {
                          CorrectAnswerNumber = q.CorrectAnswerNumber,
                          TechnologyId = q.TechnologyId
                      }).SingleAsync();
    }

    public async Task AnswerAndSaveAsync(QuestionAnswerDto answer)
    {
        int rows = await Ctx.TestQuestions
            .Where(tq => tq.TestId == answer.TestId && tq.QuestionId == answer.QuestionId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(tq => tq.AnswerNumber, answer.AnswerNumber)
                        .SetProperty(tq => tq.AnswerPoints, answer.AnswerPoints)
                        .SetProperty(tq => tq.AnswerDate, answer.AnswerDate));

        if (rows == 0)
            throw new InvalidOperationException("Question was not found");
    }

    public async Task<TestDto> GetTestByIdAsync(string userName, int? testId)
    {
        if (testId.HasValue)
        {
            return await (from t in Ctx.Tests
                          where t.Id == testId && t.Username == userName && t.FinishDate == null
                          select new TestDto
                          {
                              Id = t.Id,
                              TechnologyId = t.TechnologyId,
                              Username = t.Username,
                              StartDate = t.StartDate,
                          }).SingleAsync();
        }
        else
        {
            return await (from t in Ctx.Tests
                          where t.Username == userName && t.FinishDate == null && (DateTime.Now - t.StartDate).TotalMinutes < 90
                          orderby t.StartDate descending
                          select new TestDto
                          {
                              Id = t.Id,
                              TechnologyId = t.TechnologyId,
                              Username = t.Username,
                              StartDate = t.StartDate,
                          }).FirstOrDefaultAsync();
        }
    }

    public async Task<TestResultDto> GetTestResultAsync(string userName, int testId)
    {
        return await (from t in Ctx.Tests
                      where t.Id == testId && t.Username == userName
                      select new TestResultDto
                      {
                          Score = (float)t.FinalScore,
                          TimeSpentInSeconds = (int)(t.FinishDate.Value - t.StartDate).TotalSeconds,
                      }).SingleAsync();
    }

    public async Task CompleteTestAndSaveAsync(string userName, int testId, float finalScore)
    {
        int rows = await Ctx.Tests
            .Where(t => t.Id == testId && t.Username == userName)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(t => t.FinalScore, finalScore)
                        .SetProperty(t => t.FinishDate, DateTime.UtcNow));

        if (rows == 0)
            throw new InvalidOperationException("Test was not found");
    }

    public async Task<int?> GetActiveTestByUserAsync(string userName)
    {
        DateTime now = DateTime.UtcNow;

        int id = await (from t in Ctx.Tests
                        join tt in Ctx.Technologies on t.TechnologyId equals tt.Id
                        where t.Username == userName &&
                           !t.FinalScore.HasValue &&
                           !t.FinishDate.HasValue &&
                           EF.Functions.DateDiffMinute(t.StartDate, now) < tt.DurationInMinutes
                        orderby t.StartDate descending
                        select t.Id).FirstOrDefaultAsync();

        return id == 0 ? null : id;
    }

    public async Task<CurrentTestStateDto> GetCurrentTestStateAsync(int testId)
    {
        var data = await (from t in Ctx.Tests
                          join tq in Ctx.TestQuestions on t.Id equals tq.TestId
                          join q in Ctx.Questions on tq.QuestionId equals q.Id
                          where t.Id == testId && tq.AnswerDate == null
                          orderby tq.Id
                          select new { Question = q, tqId = tq.Id, t.StartDate, q.TechnologyId }).FirstAsync();

        var tqArray = await (from tq in Ctx.TestQuestions
                             where tq.TestId == testId
                             select new { tq.Id, tq.AnswerDate }).ToArrayAsync();

        CurrentTestStateDto dto = new();

        dto.TestId = testId;
        dto.TechnologyId = data.TechnologyId;
        dto.CurrentQuestion = new QuestionDto
        {
            TestId = testId,
            TestQuestionId = data.tqId,
            QuestionId = data.Question.Id,
            Text = data.Question.Text,
            Answer1 = data.Question.Answer1,
            Answer2 = data.Question.Answer2,
            Answer3 = data.Question.Answer3,
            Answer4 = data.Question.Answer4
        };
        dto.TotalQuestions = tqArray.Length;
        dto.Number = tqArray.Count(tq => tq.AnswerDate != null) + 1;
        dto.SpentTimeInSeconds = (int)(DateTime.UtcNow - data.StartDate).TotalSeconds;

        return dto;
    }

    public async Task<bool> CanAnswerQuestionAsync(int testId, int questionId, string userName)
    {
        DateTime now = DateTime.UtcNow;

        return await (from t in Ctx.Tests
                      join tq in Ctx.TestQuestions on t.Id equals tq.TestId
                      where t.Id == testId &&
                            t.Username == userName &&
                            t.FinalScore == null &&
                            t.FinishDate == null &&
                            tq.QuestionId == questionId &&
                            tq.AnswerDate == null &&
                            tq.RequestDate != null
                      select t.Id).AnyAsync();
    }
}

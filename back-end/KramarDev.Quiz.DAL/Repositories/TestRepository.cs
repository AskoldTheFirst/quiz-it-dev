
namespace KramarDev.Quiz.DAL.Repositories;

public class TestRepository(QuizDbContext dbCtx) : ITestRepository
{
    private readonly QuizDbContext Ctx = dbCtx;

    public async Task<int> CreateTestAsync(NewTestDto newTest)
    {
        int questionAmount = newTest.QuestionIds.Length;

        Test test = new ()
        {
            TechnologyId = newTest.TechnologyId,
            Username = newTest.Username,
            StartDate = newTest.StartDate,
            IpAddress = newTest.RemoteIpAddress,
            State = TestState.Created
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

    public async Task CompleteTestAndSaveAsync(string userName, int testId, float finalScore)
    {
        int rows = await Ctx.Tests
            .Where(t => t.Id == testId && t.Username == userName)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(t => t.FinalScore, finalScore)
                        .SetProperty(t => t.FinishDate, DateTime.UtcNow)
                        .SetProperty(t => t.State, TestState.Completed));

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
                           EF.Functions.DateDiffMinute(now, t.StartDate) < tt.DurationInMinutes
                        orderby t.StartDate descending
                        select t.Id).FirstOrDefaultAsync();

        return id == 0 ? null : id;
    }

    public async Task<CurrentTestStateDto> GetCurrentTestStateAsync(int testId)
    {
        var data = await (from t in Ctx.Tests
                          join tq in Ctx.TestQuestions on t.Id equals tq.TestId
                          join q in Ctx.Questions on tq.QuestionId equals q.Id
                          where t.Id == testId && tq.RequestDate.HasValue && tq.AnswerDate == null
                          select new { Question = q, tqId = tq.Id, t.StartDate, q.TechnologyId }).FirstOrDefaultAsync();

        if (data == null)
        {
            return null;
        }

        var tqArray = await (from tq in Ctx.TestQuestions
                             where tq.TestId == testId
                             select new { tq.Id, tq.AnswerDate }).ToArrayAsync();

        CurrentTestStateDto dto = new();

        dto.TestId = testId;
        dto.TechnologyId = data.TechnologyId;
        dto.CurrentQuestion = new QuestionDto
        {
            Number = tqArray.Count(tq => tq.AnswerDate != null) + 1,
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
        dto.SpentTimeInSeconds = (int)(DateTime.UtcNow - data.StartDate).TotalSeconds;

        return dto;
    }

    public async Task<bool> CanAnswerQuestionAsync(int testId, int questionId, string userName)
    {
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

    public async Task CancelTestAsync(string userName, int testId)
    {
        int rows = await Ctx.Tests
            .Where(t => t.Id == testId && t.Username == userName)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(t => t.State, TestState.Cancelled)
                        .SetProperty(t => t.FinishDate, DateTime.UtcNow));

        if (rows == 0)
            throw new InvalidOperationException("Test was not found");
    }
}

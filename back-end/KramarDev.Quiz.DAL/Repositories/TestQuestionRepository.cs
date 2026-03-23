using KramarDev.Quiz.DALAbstractions.Enum;

namespace KramarDev.Quiz.DAL.Repositories;

public sealed class TestQuestionRepository(QuizDbContext dbCtx) : ITestQuestionRepository
{
    private readonly QuizDbContext Ctx = dbCtx;

    public Task AnswerAndSaveAsync(QuestionAnswerDto answer)
    {
        throw new NotImplementedException();
    }

    public async Task<int[]> GetAllQuestionsAsync(string technologyName, Difficulty difficulty)
    {
        return await (from t in Ctx.Technologies
                      join q in Ctx.Questions on t.Id equals q.TechnologyId
                      where t.Name == technologyName && q.Difficulty == (byte)difficulty && q.IsActive
                      select q.Id).ToArrayAsync();
    }

    public async Task UpdateTestQuestionDateAsync(int testId, int testQuestionId)
    {
        TestQuestion nextTestQuestion = await (from tq in Ctx.TestQuestions
                                               where tq.TestId == testId && tq.Id == testQuestionId
                                               select tq).SingleAsync();

        nextTestQuestion.RequestDate = DateTime.Now;
    }

    public async Task<int> GetAmountOfAlreadyAnsweredQuestionsAsync(int testId)
    {
        return await (from q in Ctx.TestQuestions
                      where q.TestId == testId && q.AnswerDate != null
                      select q.Id).CountAsync();
    }

    public async Task<TotalScoreAndAmountDto> GetScoreAndAmountAsync(int testId)
    {
        return await (from tq in Ctx.TestQuestions
                          where tq.TestId == testId
                          group tq by 1 into grp
                          select new TotalScoreAndAmountDto
                          {
                              TotalAmount = grp.Count(),
                              TotalScore = grp.Sum(t => (int)t.AnswerPoints)
                          }).SingleAsync();
    }

    public async Task<int> GetAnsweredQuestionsCountAsync(string userName, int testId, float finalScore)
    {
        return await (from tq in Ctx.TestQuestions
                      where tq.TestId == testId && tq.AnswerDate != null
                      select tq.Id).CountAsync();
    }
}

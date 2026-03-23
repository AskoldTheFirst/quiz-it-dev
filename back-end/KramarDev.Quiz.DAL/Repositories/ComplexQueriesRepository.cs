namespace KramarDev.Quiz.DAL.Repositories;

public sealed class ComplexQueriesRepository(QuizDbContext dbCtx) : IComplexQueriesRepository
{
    private readonly QuizDbContext Ctx = dbCtx;


    public async Task<QuestionDto> SelectNextQuestionAsync(int testId, string userName)
    {
        return await (from tq in Ctx.TestQuestions
                      join q in Ctx.Questions on tq.QuestionId equals q.Id
                      join t in Ctx.Tests on tq.TestId equals t.Id
                      where tq.TestId == testId && t.Username == userName && tq.AnswerDate == null
                      orderby tq.Id descending
                      select new QuestionDto
                      {
                          Text = q.Text,
                          Answer1 = q.Answer1,
                          Answer2 = q.Answer2,
                          Answer3 = q.Answer3,
                          Answer4 = q.Answer4,
                          QuestionId = q.Id,
                          TestId = testId,
                          TestQuestionId = tq.Id
                      }).FirstOrDefaultAsync();
    }
}


namespace KramarDev.Quiz.DAL.Repositories;

public class QuestionRepository(QuizDbContext dbCtx) : IQuestionRepository
{
    private readonly QuizDbContext Ctx = dbCtx;

    public async Task<AnswerResultDto[]> GetAnswersAsync(int testId, string userName)
    {
        return await (from tq in Ctx.TestQuestions
                      join q in Ctx.Questions on tq.QuestionId equals q.Id
                      join t in Ctx.Tests on tq.TestId equals t.Id
                      where tq.TestId == testId && t.Username == userName && tq.AnswerDate != null
                      orderby tq.Id
                      select new AnswerResultDto
                      {
                          QuestionText = q.Text,
                          Answer = tq.AnswerNumber == 1 ? q.Answer1
                                   : tq.AnswerNumber == 2 ? q.Answer2
                                   : tq.AnswerNumber == 3 ? q.Answer3
                                   : tq.AnswerNumber == 4 ? q.Answer4
                                   : null,
                          CorrectAnswer = q.CorrectAnswerNumber == 1 ? q.Answer1
                                   : q.CorrectAnswerNumber == 2 ? q.Answer2
                                   : q.CorrectAnswerNumber == 3 ? q.Answer3
                                   : q.CorrectAnswerNumber == 4 ? q.Answer4
                                   : null,
                          Complexity = q.Difficulty
                      }).ToArrayAsync();
    }
}

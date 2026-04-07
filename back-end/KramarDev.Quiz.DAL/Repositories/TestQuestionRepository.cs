
namespace KramarDev.Quiz.DAL.Repositories;

public sealed class TestQuestionRepository(QuizDbContext dbCtx) : ITestQuestionRepository
{
    private readonly QuizDbContext Ctx = dbCtx;

    public async Task<int[]> GetAllQuestionsAsync(string topicName, Difficulty difficulty)
    {
        return await (from t in Ctx.Topics
                      join q in Ctx.Questions on t.Id equals q.TopicId
                      where t.Name == topicName && q.Difficulty == (byte)difficulty && q.IsActive
                      select q.Id).ToArrayAsync();
    }

    public async Task UpdateTestQuestionDateAsync(int testId, int testQuestionId)
    {
        TestQuestion nextTestQuestion = await (from tq in Ctx.TestQuestions
                                               where tq.TestId == testId && tq.Id == testQuestionId
                                               select tq).SingleAsync();

        nextTestQuestion.RequestDate = DateTime.UtcNow;
    }

    public async Task<int> GetAnsweredQuestionsCountAsync(string userName, int testId, float finalScore)
    {
        return await (from tq in Ctx.TestQuestions
                      where tq.TestId == testId && tq.AnswerDate != null
                      select tq.Id).CountAsync();
    }
}

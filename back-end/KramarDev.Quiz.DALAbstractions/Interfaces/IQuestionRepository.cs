
namespace KramarDev.Quiz.DALAbstractions.Repositories;

public interface IQuestionRepository
{
    Task<AnswerResultDto[]> GetAnswersAsync(int testId, string userName);
}
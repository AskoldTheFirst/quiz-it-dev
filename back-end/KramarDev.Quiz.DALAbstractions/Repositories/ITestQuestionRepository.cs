using KramarDev.Quiz.DALAbstractions.Enum;

namespace KramarDev.Quiz.DALAbstractions.Repositories;

public interface ITestQuestionRepository
{
    Task AnswerAndSaveAsync(QuestionAnswerDto answer);

    Task<int[]> GetAllQuestionsAsync(string technologyName, Difficulty difficulty);

    Task UpdateTestQuestionDateAsync(int testId, int testQuestionId);

    Task<int> GetAmountOfAlreadyAnsweredQuestionsAsync(int testId);

    Task<TotalScoreAndAmountDto> GetScoreAndAmountAsync(int testId);
}

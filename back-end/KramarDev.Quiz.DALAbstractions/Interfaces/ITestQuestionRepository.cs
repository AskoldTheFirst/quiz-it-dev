using KramarDev.Quiz.DALAbstractions.Enum;

namespace KramarDev.Quiz.DALAbstractions.Repositories;

public interface ITestQuestionRepository
{
    Task<int[]> GetAllQuestionsAsync(string technologyName, Difficulty difficulty);

    Task UpdateTestQuestionDateAsync(int testId, int testQuestionId);
}

namespace KramarDev.Quiz.BLLAbstractions.Interfaces;

public interface IAppCacheService
{
    Task<int[]> GetEasyQuestionIdsAsync(string technologyName);

    Task<int[]> GetMiddleQuestionIdsAsync(string technologyName);

    Task<int[]> GetHardQuestionIdsAsync(string technologyName);

    Task<TechnologyDto[]> GetTechnologiesAsync();

    Task<TechnologyDto> GetTechnologyByNameAsync(string name);

    Task<TechnologyDto> GetTechnologyByIdAsync(int id);
}

namespace KramarDev.Quiz.DALAbstractions.Repositories;

public interface ITechnologyRepository
{
    Task<TechnologyDto[]> GetTechnologiesAsync();

    Task<TechnologyDto> GetTechnologyByTestIdAsync(int testId);
}

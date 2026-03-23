using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace KramarDev.Quiz.BLL;

public class AppCacheService(IServiceScopeFactory scopeFactory, IMemoryCache cacheService) : IAppCacheService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly IMemoryCache _cache = cacheService;

    public async Task<int[]> GetEasyQuestionIdsAsync(string technologyName)
    {
        return await GetIdsAsync(technologyName, Difficulty.Easy);
    }

    public async Task<int[]> GetMiddleQuestionIdsAsync(string technologyName)
    {
        return await GetIdsAsync(technologyName, Difficulty.Medium);
    }

    public async Task<int[]> GetHardQuestionIdsAsync(string technologyName)
    {
        return await GetIdsAsync(technologyName, Difficulty.Hard);
    }

    private async Task<int[]> GetIdsAsync(string technologyName, Difficulty diff)
    {
        string key = $"ids-{diff}-{technologyName}";
        int[] ids;
        if (!_cache.TryGetValue(key, out ids))
        {
            ids = await GetUoW().TestQuestionRepository.GetAllQuestionsAsync(technologyName, diff);
            _cache.Set(key, ids);
        }
        return ids;
    }

    public async Task<TechnologyDto[]> GetTechnologiesAsync()
    {
        string key = $"technologyDbTable";
        TechnologyDto[] technologies;

        if (!_cache.TryGetValue(key, out technologies))
        {
            IUnitOfWork unitOfWork = GetUoW();
            var dalTechnologies = await unitOfWork.TechnologyRepository.GetTechnologiesAsync();
            technologies = DtoMapper.FromDAL(dalTechnologies);
            _cache.Set(key, technologies);
        }

        return technologies;
    }

    public async Task<TechnologyDto> GetTechnologyByNameAsync(string name)
    {
        TechnologyDto[] technologies = await GetTechnologiesAsync();
        for (int i = 0; i < technologies.Length; ++i)
            if (String.Compare(technologies[i].Name, name, true) == 0)
                return technologies[i];

        return null;
    }

    public async Task<TechnologyDto> GetTechnologyByIdAsync(int id)
    {
        TechnologyDto[] technologies = await GetTechnologiesAsync();
        for (int i = 0; i < technologies.Length; ++i)
            if (technologies[i].Id == id)
                return technologies[i];

        return null;
    }

    private IUnitOfWork GetUoW()
    {
        return _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IUnitOfWork>();
    }
}

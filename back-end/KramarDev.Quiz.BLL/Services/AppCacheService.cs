using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace KramarDev.Quiz.BLL.Services;

public class AppCacheService(IServiceScopeFactory scopeFactory, IMemoryCache cacheService) : IAppCacheService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly IMemoryCache _cache = cacheService;

    public async Task<int[]> GetEasyQuestionIdsAsync(string topicName)
    {
        return await GetIdsAsync(topicName, Difficulty.Easy);
    }

    public async Task<int[]> GetMiddleQuestionIdsAsync(string topicName)
    {
        return await GetIdsAsync(topicName, Difficulty.Medium);
    }

    public async Task<int[]> GetHardQuestionIdsAsync(string topicName)
    {
        return await GetIdsAsync(topicName, Difficulty.Hard);
    }

    private async Task<int[]> GetIdsAsync(string topicName, Difficulty diff)
    {
        string key = $"ids-{diff}-{topicName}";
        int[] ids;
        if (!_cache.TryGetValue(key, out ids))
        {
            await using IUnitOfWork uow = GetUoW();
            ids = await uow.TestQuestionRepository.GetAllQuestionsAsync(topicName, diff);
            _cache.Set(key, ids);
        }
        return ids;
    }

    public async Task<TopicDto[]> GetTopicsAsync()
    {
        string key = $"topicDbTable";
        TopicDto[] topics;

        if (!_cache.TryGetValue(key, out topics))
        {
            await using IUnitOfWork uow = GetUoW();
            var dalTopics = await uow.TopicRepository.GetTopicsAsync();
            topics = DtoMapper.FromDAL(dalTopics);
            _cache.Set(key, topics);
        }

        return topics;
    }

    public async Task<TopicDto> GetTopicByNameAsync(string name)
    {
        TopicDto[] topics = await GetTopicsAsync();
        for (int i = 0; i < topics.Length; ++i)
            if (String.Compare(topics[i].Name, name, true) == 0)
                return topics[i];

        return null;
    }

    public async Task<TopicDto> GetTopicByIdAsync(int id)
    {
        TopicDto[] topics = await GetTopicsAsync();
        for (int i = 0; i < topics.Length; ++i)
            if (topics[i].Id == id)
                return topics[i];

        return null;
    }

    private IUnitOfWork GetUoW()
    {
        return _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IUnitOfWork>();
    }
}

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace KramarDev.Quiz.BLL.Services;

public sealed class AppCacheService(IServiceScopeFactory scopeFactory, IMemoryCache cacheService) : IAppCacheService
{
    private const string KeyTopics = "topics";

    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly IMemoryCache _cache = cacheService;

    private readonly SemaphoreSlim _semaphoreForIds = new(1, 1);
    private readonly SemaphoreSlim _semaphoreForTopics = new(1, 1);

    // PUBLIC API
    public async Task InitializeCacheAsync()
    {
        TopicDto[] topics;

        await using (var scope = _scopeFactory.CreateAsyncScope())
        {
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var dalTopics = await uow.TopicRepository.GetTopicsAsync();
            topics = DtoMapper.FromDAL(dalTopics);
        }

        _cache.Set(KeyTopics, topics);

        Task[] tasks = topics.Select(topic => InitializeTopicIdsAsync(topic.Name)).ToArray();
        await Task.WhenAll(tasks);
    }

    public ValueTask<int[]> GetEasyQuestionIdsAsync(string topicName)
    {
        return GetIdsAsync(topicName, Difficulty.Easy);
    }

    public ValueTask<int[]> GetMiddleQuestionIdsAsync(string topicName)
    {
        return GetIdsAsync(topicName, Difficulty.Medium);
    }

    public ValueTask<int[]> GetHardQuestionIdsAsync(string topicName)
    {
        return GetIdsAsync(topicName, Difficulty.Hard);
    }

    public ValueTask<TopicDto[]> GetTopicsAsync()
    {
        if (_cache.TryGetValue(KeyTopics, out TopicDto[] topics))
        {
            return new ValueTask<TopicDto[]>(topics);
        }

        return new ValueTask<TopicDto[]>(LoadTopicsAsync());
    }

    public async Task<TopicDto> GetTopicByNameAsync(string name)
    {
        TopicDto[] topics = await GetTopicsAsync();
        for (int i = 0; i < topics.Length; ++i)
            if (string.Equals(topics[i].Name, name, StringComparison.OrdinalIgnoreCase))
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

    // PRIVATE HELPERS
    private ValueTask<int[]> GetIdsAsync(string topicName, Difficulty diff)
    {
        string key = GetCacheKeyForIds(topicName, diff);
        if (_cache.TryGetValue(key, out int[] ids))
        {
            return new ValueTask<int[]>(ids);
        }

        return new ValueTask<int[]>(LoadIdsAsync(topicName, diff, key));
    }

    private async Task InitializeTopicIdsAsync(string topicName)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        int[] easy = await uow.QuestionRepository.GetAllQuestionsAsync(topicName, Difficulty.Easy);
        int[] medium = await uow.QuestionRepository.GetAllQuestionsAsync(topicName, Difficulty.Medium);
        int[] hard = await uow.QuestionRepository.GetAllQuestionsAsync(topicName, Difficulty.Hard);

        _cache.Set(GetCacheKeyForIds(topicName, Difficulty.Easy), easy);
        _cache.Set(GetCacheKeyForIds(topicName, Difficulty.Medium), medium);
        _cache.Set(GetCacheKeyForIds(topicName, Difficulty.Hard), hard);
    }

    private async Task<TopicDto[]> LoadTopicsAsync()
    {
        /*  
         *  Defensive fallback: cache should be pre-initialized at startup,
         *  but this ensures only one concurrent DB load if a miss happens.
         */
        await _semaphoreForTopics.WaitAsync();

        try
        {
            if (_cache.TryGetValue(KeyTopics, out TopicDto[] cacheTopics))
                return cacheTopics;

            await using var scope = _scopeFactory.CreateAsyncScope();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var dalTopics = await uow.TopicRepository.GetTopicsAsync();
            TopicDto[] topics = DtoMapper.FromDAL(dalTopics);
            _cache.Set(KeyTopics, topics);
            return topics;
        }
        finally
        {
            _semaphoreForTopics.Release();
        }
    }

    private async Task<int[]> LoadIdsAsync(string topicName, Difficulty diff, string key)
    {
        /*  
         *  Defensive fallback: cache should be pre-initialized at startup,
         *  but this ensures only one concurrent DB load if a miss happens.
         */
        await _semaphoreForIds.WaitAsync();

        try
        {
            if (_cache.TryGetValue(key, out int[] cacheIds))
                return cacheIds;

            await using var scope = _scopeFactory.CreateAsyncScope();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            int[] ids = await uow.QuestionRepository.GetAllQuestionsAsync(topicName, diff);
            _cache.Set(key, ids);
            return ids;
        }
        finally
        {
            _semaphoreForIds.Release();
        }
    }

    private static string GetCacheKeyForIds(string topicName, Difficulty diff)
    {
        return $"ids-{diff}-{topicName}";
    }
}

namespace KramarDev.Quiz.BLLAbstractions.Interfaces;

public interface IAppCacheService
{
    Task InitializeCacheAsync();

    ValueTask<int[]> GetEasyQuestionIdsAsync(string topicName);

    ValueTask<int[]> GetMiddleQuestionIdsAsync(string topicName);

    ValueTask<int[]> GetHardQuestionIdsAsync(string topicName);

    ValueTask<TopicDto[]> GetTopicsAsync();

    Task<TopicDto> GetTopicByNameAsync(string name);

    Task<TopicDto> GetTopicByIdAsync(int id);
}

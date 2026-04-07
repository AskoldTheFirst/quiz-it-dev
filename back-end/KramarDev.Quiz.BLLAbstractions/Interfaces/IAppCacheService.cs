namespace KramarDev.Quiz.BLLAbstractions.Interfaces;

public interface IAppCacheService
{
    Task<int[]> GetEasyQuestionIdsAsync(string topicName);

    Task<int[]> GetMiddleQuestionIdsAsync(string topicName);

    Task<int[]> GetHardQuestionIdsAsync(string topicName);

    Task<TopicDto[]> GetTopicsAsync();

    Task<TopicDto> GetTopicByNameAsync(string name);

    Task<TopicDto> GetTopicByIdAsync(int id);
}

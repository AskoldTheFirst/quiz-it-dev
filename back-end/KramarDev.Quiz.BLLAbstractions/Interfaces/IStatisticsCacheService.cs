namespace KramarDev.Quiz.BLLAbstractions.Interfaces;

public interface IStatisticsCacheService
{
    //Task InitializeCacheAsync();

    ValueTask<StatisticsPageDto> GetPageAsync(StatisticsRequestDto requestDto);

    //ValueTask<int[]> GetEasyQuestionIdsAsync(string topicName);

    //ValueTask<int[]> GetMiddleQuestionIdsAsync(string topicName);

    //ValueTask<int[]> GetHardQuestionIdsAsync(string topicName);

    //ValueTask<TopicDto[]> GetTopicsAsync();

    //Task<TopicDto> GetTopicByNameAsync(string name);

    //Task<TopicDto> GetTopicByIdAsync(int id);
}

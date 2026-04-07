namespace KramarDev.Quiz.DALAbstractions.Repositories;

public interface ITopicRepository
{
    Task<TopicDto[]> GetTopicsAsync();

    Task<TopicDto> GetTopicByTestIdAsync(int testId);
}

namespace KramarDev.Quiz.BLLAbstractions.Interfaces;

public interface IStatisticsService
{
    Task<StatisticsPageDto> SelectByFilterAsync(int topicId, int scoreThreshold, int pageSize, int pageNumber);

    Task<ProfileDto> GetProfileAsync(string userName);
}

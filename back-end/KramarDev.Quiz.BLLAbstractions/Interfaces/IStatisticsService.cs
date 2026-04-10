namespace KramarDev.Quiz.BLLAbstractions.Interfaces;

public interface IStatisticsService
{
    Task<ProfileDto> GetProfileAsync(string userName);

    Task<StatisticsPageDto> SelectByFilterAsync(int topicId, int scoreThreshold, int pageSize, int pageNumber);

    Task HideAsync(string userName, CancellationToken cancellationToken);
}

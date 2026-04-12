namespace KramarDev.Quiz.BLLAbstractions.Interfaces;

public interface IStatisticsService
{
    Task<ProfileDto> GetProfileAsync(string userName, CancellationToken cancellationToken = default);

    Task<StatisticsPageDto> GetStatisticsPageAsync(int topicId, int scoreThreshold, int pageSize,
        int pageNumber, CancellationToken cancellationToken = default);
}

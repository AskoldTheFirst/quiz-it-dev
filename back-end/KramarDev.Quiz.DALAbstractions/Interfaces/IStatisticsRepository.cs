
namespace KramarDev.Quiz.DALAbstractions.Interfaces;

public interface IStatisticsRepository
{
    Task<RowDto[]> SelectByFilterAsync(int topicId, int scoreThreshold, int pageSize, int pageNumber);

    Task<int> GetTotalCountAsync(int topicId, int scoreThreshold);

    Task<ProfileDto> GetProfileAsync(string userName);
}

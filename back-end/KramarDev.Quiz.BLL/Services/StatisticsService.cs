namespace KramarDev.Quiz.BLL.Services;

public class StatisticsService(IUnitOfWork uow) : IStatisticsService
{
    private readonly IUnitOfWork _uow = uow;


    public async Task<StatisticsPageDto> SelectByFilterAsync(
        int topicId, int scoreThreshold, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        int startIndex = pageNumber * pageSize + 1;

        var dalRows = await _uow.StatisticsRepository.SelectByFilterAsync(
            topicId, scoreThreshold, pageSize, pageNumber, cancellationToken);

        var totalCount = await _uow.StatisticsRepository.GetTotalCountAsync(
            topicId, scoreThreshold, cancellationToken);

        RowDto[] rows = DtoMapper.FromDAL(dalRows, startIndex);

        return new StatisticsPageDto
        {
            Rows = rows,
            TotalCount = totalCount
        };
    }

    public async Task<ProfileDto> GetProfileAsync(string userName, CancellationToken cancellationToken = default)
    {
        return DtoMapper.FromDAL(
            await _uow.StatisticsRepository.GetProfileAsync(userName, cancellationToken));
    }
}

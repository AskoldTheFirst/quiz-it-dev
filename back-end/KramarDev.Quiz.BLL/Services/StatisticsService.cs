namespace KramarDev.Quiz.BLL.Services;

public class StatisticsService(IUnitOfWork uow) : IStatisticsService
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<StatisticsPageDto> SelectByFilterAsync(
        int topicId, int scoreThreshold, int pageSize, int pageNumber)
    {
        int startIndex = pageNumber * pageSize + 1;

        RowDto[] rows = DtoMapper.FromDAL(
            await _uow.StatistcsRepository.SelectByFilterAsync(
                topicId, scoreThreshold, pageSize, pageNumber), startIndex);

        return new StatisticsPageDto
        {
            Rows = rows,
            TotalCount = await _uow.StatistcsRepository.GetTotalCountAsync(topicId, scoreThreshold)
        };
    }

    public async Task<ProfileDto> GetProfileAsync(string userName)
    {
        return DtoMapper.FromDAL(
            await _uow.StatistcsRepository.GetProfileAsync(userName));
    }

    public async Task HideAsync(string userName, CancellationToken cancellationToken)
    {
        await _uow.StatistcsRepository.HideTestsForUserAsync(userName, cancellationToken);
    }
}

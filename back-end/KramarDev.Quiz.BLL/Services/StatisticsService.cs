namespace KramarDev.Quiz.BLL.Services;

public sealed class StatisticsService(IUnitOfWork uow, IStatisticsCacheService cacheService) : IStatisticsService
{
    private readonly IUnitOfWork _uow = uow;
    private readonly IStatisticsCacheService _cacheService = cacheService;

    public async Task<StatisticsPageDto> GetStatisticsPageAsync(
        StatisticsRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        return await _cacheService.GetPageAsync(requestDto);
    }

    public async Task<ProfileDto> GetProfileAsync(string userName, CancellationToken cancellationToken = default)
    {
        return DtoMapper.FromDAL(
            await _uow.StatisticsRepository.GetProfileAsync(userName, cancellationToken));
    }
}

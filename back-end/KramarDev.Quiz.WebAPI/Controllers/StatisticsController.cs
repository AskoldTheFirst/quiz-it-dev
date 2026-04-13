using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KramarDev.Quiz.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController(IStatisticsService statisticsService, ITestService testService) : BaseController
{
    private readonly IStatisticsService _statisticsService = statisticsService;
    private readonly ITestService _testService = testService;

    [HttpGet("page")]
    public async Task<ActionResult<StatisticsPageModel>> Page(
        [FromQuery] StatisticsRequestModel param, CancellationToken cancellationToken)
    {
        StatisticsRequestDto bizParam = StatisticsRequestModel.ToBLL(param);

        return StatisticsPageModel.FromBLL(
            await _statisticsService.GetStatisticsPageAsync(bizParam, cancellationToken));
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<ActionResult<ProfileModel>> Profile(CancellationToken cancellationToken)
    {
        return ProfileModel.FromBLL(
            await _statisticsService.GetProfileAsync(UserName, cancellationToken));
    }

    [Authorize]
    [HttpPut("hide")]
    public async Task<ActionResult<ProfileModel>> Hide(CancellationToken cancellationToken)
    {
        await _testService.HideAsync(UserName, cancellationToken);
        return ProfileModel.FromBLL(
            await _statisticsService.GetProfileAsync(UserName, cancellationToken));
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KramarDev.Quiz.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController(IStatisticsService statisticsService) : BaseController
{
    private readonly IStatisticsService _statisticsService = statisticsService;

    [HttpGet("page")]
    public async Task<ActionResult<StatisticsPageModel>> Page([FromQuery] StatisticsRequestModel param)
    {
        return StatisticsPageModel.FromBLL(
            await _statisticsService.SelectByFilterAsync(
                param.TopicId, param.ScoreThreshold, param.PageSize, param.PageNumber));
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<ActionResult<ProfileModel>> Profile()
    {
        return ProfileModel.FromBLL(
            await _statisticsService.GetProfileAsync(UserName));
    }
}

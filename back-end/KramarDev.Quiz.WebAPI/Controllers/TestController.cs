using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KramarDev.Quiz.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestsController(ITestService testService) : BaseController
{
    private readonly ITestService _testService = testService;


    [Authorize]
    [HttpGet("current")]
    public async Task<ActionResult<TestModel>> Current(CancellationToken cancellationToken)
    {
        TestDto testDto = await _testService.RestoreCurrentTestAsync(UserName, cancellationToken);

        if (testDto == null)
            return Ok(null);

        return Ok(TestModel.FromBL(testDto));
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<TestModel>> Create([FromQuery] string topicName, CancellationToken cancellationToken)
    {
        TestDto testDto = await _testService.CreateTestAsync(
            topicName, UserName, IpAddress, cancellationToken);

        return Ok(TestModel.FromBL(testDto));
    }

    [Authorize]
    [HttpPost("answer")]
    public async Task<ActionResult<AnswerResponseModel>> Answer([FromBody] AnswerRequestModel requestModel)
    {
        AnswerResponseDto responseDto = await _testService.AnswerAndNextAsync(
            requestModel.TestId, requestModel.QuestionId, requestModel.AnswerNumber, UserName);

        return Ok(AnswerResponseModel.FromBLL(responseDto));
    }

    [Authorize]
    [HttpPost("cancel")]
    public async Task<ActionResult> Cancel([FromQuery] int testId)
    {
        await _testService.CancelTestAsync(UserName, testId);
        return Ok();
    }

    [Authorize]
    [HttpPost("complete")]
    public async Task<ActionResult<TestResultModel>> Complete([FromQuery] int testId)
    {
        TestResultDto resultDto = await _testService.CompleteAsync(testId, UserName);
        return Ok(TestResultModel.FromBLL(resultDto));
    }
}
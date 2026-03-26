using KramarDev.Quiz.WebAPI.Model;
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
    public async Task<ActionResult<TestModel>> GetCurrentTest()
    {
        TestDto testDto = await _testService.RestoreCurrentTestAsync(UserName);

        if (testDto == null)
            return NotFound();

        return Ok(TestModel.FromBL(testDto));
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<TestModel>> CreateTest([FromQuery] string technologyName)
    {
        TestDto test = await _testService.CreateTestAsync(
            technologyName, UserName, IpAddress);

        return Ok(TestModel.FromBL(test));
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
    public async Task<ActionResult> CancelTest(int testId)
    {
        string userName = User.Identity.Name;

        await _testService.CancelTestAsync(userName, testId);
        return Ok();
    }
}
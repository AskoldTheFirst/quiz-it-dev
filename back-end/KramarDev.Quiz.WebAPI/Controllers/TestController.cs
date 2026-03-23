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
    public async Task<ActionResult<CurrentTestModel>> GetCurrentTest()
    {
        CurrentTestDto testDto = await _testService.RestoreCurrentTestAsync(UserName);
        if (testDto == null)
            return NotFound();

        return Ok(CurrentTestModel.FromBL(testDto));
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<TestCreatedModel>> CreateTest([FromQuery] string technologyName)
    {
        TestCreatedDto testInfo = await _testService.CreateTestAsync(
            technologyName, UserName, IpAddress);

        return Ok(TestCreatedModel.FromBL(testInfo));
    }

    [Authorize]
    [HttpGet("next-question")]
    public async Task<ActionResult<QuestionModel>> NextQuestion(int testId)
    {
        return Ok(QuestionModel.FromBLL(await _testService.GetNextQuestionAsync(testId, UserName)));
    }

    [Authorize]
    [HttpPost("answer")]
    public async Task<ActionResult> Answer(int testId, int questionId, byte answerNumber)
    {
        await _testService.AnswerAsync(testId, questionId, answerNumber, UserName);
        return Ok();
    }

    [Authorize]
    [HttpGet("next-question-state")]
    public async Task<ActionResult<NextQuestionStateModel>> NextQuestionState(int? testId)
    {
        string userName = User.Identity.Name;
        NextQuestionStateDto stateDto = await _testService.GetNextQuestionStateAsync(userName, testId);
        return Ok(DtoMapper.FromBLL(stateDto));
    }

    [Authorize]
    [HttpGet("test-result")]
    public async Task<ActionResult<TestResultModel>> TestResult(int testId)
    {
        string userName = User.Identity.Name;
        TestResultDto resultDto = await _testService.GetTestResultAsync(userName, testId);
        return Ok(DtoMapper.FromBLL(resultDto));
    }

    [Authorize]
    [HttpPut("complete-test")]
    public async Task<ActionResult<TestResultModel>> CompleteTestAndRetrieveResult(int testId)
    {
        string userName = User.Identity.Name;

        TestResultDto resultDto = await _testService.GetTestResultAsync(userName, testId);
        return Ok(DtoMapper.FromBLL(resultDto));
    }
}
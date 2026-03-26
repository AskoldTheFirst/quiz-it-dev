
namespace KramarDev.Quiz.BLLAbstractions.Dto;

public class AnswerResponseDto
{
    public QuestionDto NextQuestion { get; set; }

    public TestResultDto TestResult { get; set; }
}

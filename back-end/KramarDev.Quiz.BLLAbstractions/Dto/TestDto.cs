
namespace KramarDev.Quiz.BLLAbstractions.Dto;

public sealed class TestDto
{
    public int TestId { get; set; }

    public int QuestionCount { get; set; }

    public int SecondsLeft { get; set; }

    public string TechnologyName { get; set; }

    public QuestionDto Question { get; set; }
}

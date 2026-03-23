
namespace KramarDev.Quiz.BLLAbstractions.Dto;

public sealed class CurrentTestDto
{
    public int TestId { get; set; }

    public int Number { get; set; }

    public int TotalQuestions { get; set; }

    public int SpentTimeInSeconds { get; set; }

    public QuestionDto Question { get; set; }

    public string TestName { get; set; }

    public string TestColor { get; set; }
}

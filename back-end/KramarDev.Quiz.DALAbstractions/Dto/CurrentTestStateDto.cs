
namespace KramarDev.Quiz.DALAbstractions.Dto;

public sealed class CurrentTestStateDto
{
    public int TestId { get; set; }

    public int TechnologyId { get; set; }

    public int TotalQuestions { get; set; }

    public int SpentTimeInSeconds { get; set; }

    public QuestionDto CurrentQuestion { get; set; }
}

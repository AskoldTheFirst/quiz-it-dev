
namespace KramarDev.Quiz.DALAbstractions.Dto;

public sealed record CurrentTestStateDto
{
    public int TestId { get; init; }

    public int TopicId { get; init; }

    public int TotalQuestions { get; init; }

    public int SpentTimeInSeconds { get; init; }

    public QuestionDto CurrentQuestion { get; init; }
}

namespace KramarDev.Quiz.BLLAbstractions.Dto;

public sealed class TestResultDto
{
    public string TechnologyName { get; set; }

    public float FinalScore { get; set; }

    public int TotalPoints { get; set; }

    public int EarnedPoints { get; set; }

    public int AnsweredCount { get; set; }

    public AnswerDto[] Answers { get; set; }
}

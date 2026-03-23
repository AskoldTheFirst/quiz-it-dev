namespace KramarDev.Quiz.BLLAbstractions.Dto;

public sealed class TestResultDto
{
    public float Score { get; set; }

    public int TimeSpentInSeconds { get; set; }

    public int QuestionsAmount { get; set; }
}

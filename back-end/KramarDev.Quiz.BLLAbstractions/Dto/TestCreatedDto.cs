namespace KramarDev.Quiz.BLLAbstractions.Dto;

public sealed class TestCreatedDto
{
    public int TestId { get; set; }

    public int QuestionsAmount { get; set; }

    public int TestDurationInSeconds { get; set; }

    public string TechnologyName { get; set; }

    public string TestColor { get; set; }

    public QuestionDto FirstQuestion { get; set; }
}

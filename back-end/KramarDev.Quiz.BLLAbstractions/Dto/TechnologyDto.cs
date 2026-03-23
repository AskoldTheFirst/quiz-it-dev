namespace KramarDev.Quiz.BLLAbstractions.Dto;

public sealed class TechnologyDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int QuestionCount { get; set; }

    public int DurationInMinutes { get; set; }

    public string Color { get; set; }

    public string IconName { get; set; }
}

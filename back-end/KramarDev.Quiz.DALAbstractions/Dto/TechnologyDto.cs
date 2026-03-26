namespace KramarDev.Quiz.DALAbstractions.Dto;

public sealed class TechnologyDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int QuestionCount { get; set; }

    public int DurationInMinutes { get; set; }
}

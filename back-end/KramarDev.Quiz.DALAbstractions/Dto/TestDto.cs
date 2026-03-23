namespace KramarDev.Quiz.DALAbstractions.Dto;

public sealed class TestDto
{
    public int Id { get; set; }

    public int TechnologyId { get; set; }

    public string Username { get; set; }

    public DateTime StartDate { get; set; }

    public string RemoteIpAddress { get; set; }
}

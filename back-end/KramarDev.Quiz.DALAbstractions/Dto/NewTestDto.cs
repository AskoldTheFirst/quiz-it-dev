namespace KramarDev.Quiz.DALAbstractions.Dto;

public sealed class NewTestDto
{
    public int TechnologyId { get; set; }

    public string Username { get; set; }

    public DateTime StartDate { get; set; }

    public string RemoteIpAddress { get; set; }

    public int[] QuestionIds { get; set; }
}

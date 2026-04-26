
namespace KramarDev.Quiz.DALAbstractions.Dto;

public sealed record TestDto
{
    public int TopicId { get; set; }

    public string Username { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? FinishDate { get; set; }

    public string IpAddress { get; set; }

    public bool IsHidden { get; set; }
}

namespace KramarDev.Quiz.WebAPI.Model;

public sealed class StatisticsRequestModel
{
    public int TopicId { get; set; }

    public int ScoreThreshold { get; set; }

    public int PageSize { get; set; }

    public int PageNumber { get; set; }
}

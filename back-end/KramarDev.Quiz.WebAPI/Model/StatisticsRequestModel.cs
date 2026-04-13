namespace KramarDev.Quiz.WebAPI.Model;

public sealed class StatisticsRequestModel
{
    public int TopicId { get; set; }

    public int ScoreThreshold { get; set; }

    public int PageSize { get; set; }

    public int PageNumber { get; set; }

    public static StatisticsRequestDto ToBLL(StatisticsRequestModel model)
    {
        return new StatisticsRequestDto(model.TopicId, model.ScoreThreshold, model.PageSize, model.PageNumber);
    }
}

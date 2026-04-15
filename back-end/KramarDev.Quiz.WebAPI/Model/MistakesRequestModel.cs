namespace KramarDev.Quiz.WebAPI.Model;

public sealed record MistakesRequestModel
{
    public int TopicId { get; init; }

    public bool ByTotal { get; init; }

    public int TopCount { get; init; }

    public static MistakesRequestDto ToBLL(MistakesRequestModel model)
    {
        return new MistakesRequestDto(model.TopicId, model.ByTotal, model.TopCount);
    }
}

namespace KramarDev.Quiz.WebAPI.Model.Request;

public sealed record CreateTestRequest
{
    public string TopicName { get; init; }
}

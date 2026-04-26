namespace KramarDev.Quiz.WebAPI.Model.Request;

public sealed record AnswerRequest
{
    public int TestId { get; init; }

    public int QuestionId { get; init; }

    public byte AnswerNumber { get; init; }
}

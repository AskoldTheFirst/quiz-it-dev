namespace KramarDev.Quiz.WebAPI.Model;

public sealed record AnswerRequestModel
{
    public int TestId { get; init; }

    public int QuestionId { get; init; }

    public byte AnswerNumber { get; init; }
}

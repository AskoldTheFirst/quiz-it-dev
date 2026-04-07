namespace KramarDev.Quiz.DALAbstractions.Dto;

public sealed record QuestionInfoDto
{
    public int CorrectAnswerNumber { get; init; }

    public int TopicId { get; init; }
}

namespace KramarDev.Quiz.DALAbstractions.Dto;

public sealed class QuestionAnswerDto
{
    public int TestId { get; set; }

    public int QuestionId { get; set; }

    public byte AnswerNumber { get; set; }

    public byte AnswerPoints { get; set; }

    public DateTime AnswerDate { get; set; }
}
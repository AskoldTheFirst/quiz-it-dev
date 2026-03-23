namespace KramarDev.Quiz.BLLAbstractions.Dto;

public sealed class NextQuestionStateDto
{
    public QuestionDto Question { get; set; }

    public int QuestionNumber { get; set; }

    public int TotalAmount { get; set; }

    public int SecondsLeft { get; set; }

    public string TechnologyName { get; set; }
}

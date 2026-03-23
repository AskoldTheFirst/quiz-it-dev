namespace KramarDev.Quiz.WebAPI.Model;

public sealed class NextQuestionStateModel
{
    public QuestionModel Question { get; set; }

    public int QuestionNumber { get; set; }

    public int TotalAmount { get; set; }

    public int SecondsLeft { get; set; }

    public string TechnologyName { get; set; }
}

namespace KramarDev.Quiz.WebAPI.Model;

public class AnswerRequestModel
{
    public int TestId { get; set; }

    public int QuestionId { get; set; }

    public byte AnswerNumber { get; set; }
}

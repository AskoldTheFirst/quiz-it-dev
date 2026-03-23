namespace KramarDev.Quiz.WebAPI.Model;

public sealed class QuestionModel
{
    public int TestId { get; set; }

    public int QuestionId { get; set; }

    public int TestQuestionId { get; set; }

    public string Text { get; set; }

    public string Answer1 { get; set; }

    public string Answer2 { get; set; }

    public string Answer3 { get; set; }

    public string Answer4 { get; set; }

    public static QuestionModel FromBLL(QuestionDto questionDto)
    {
        return new QuestionModel
        {
            QuestionId = questionDto.QuestionId,
            TestId = questionDto.TestId,
            TestQuestionId = questionDto.TestQuestionId,
            Text = questionDto.Text,
            Answer1 = questionDto.Answer1,
            Answer2 = questionDto.Answer2,
            Answer3 = questionDto.Answer3,
            Answer4 = questionDto.Answer4
        };
    }
}

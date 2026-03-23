namespace KramarDev.Quiz.WebAPI.Model;

public class CurrentTestModel
{
    public string TestName { get; set; }

    public string TestColor { get; set; }

    public int TestId { get; set; }

    public int Number { get; set; }

    public int TotalQuestions { get; set; }

    public int SpentTimeInSeconds { get; set; }

    public int QuestionId { get; set; }

    public int TestQuestionId { get; set; }

    public string QuestionText { get; set; }

    public string QuestionAnswer1 { get; set; }

    public string QuestionAnswer2 { get; set; }

    public string QuestionAnswer3 { get; set; }

    public string QuestionAnswer4 { get; set; }

    public static CurrentTestModel FromBL(CurrentTestDto dto)
    {
        return new CurrentTestModel
        {
            Number = dto.Number,
            QuestionText = dto.Question.Text,
            QuestionAnswer1 = dto.Question.Answer1,
            QuestionAnswer2 = dto.Question.Answer2,
            QuestionAnswer3 = dto.Question.Answer3,
            QuestionAnswer4 = dto.Question.Answer4,
            TestQuestionId = dto.Question.TestQuestionId,
            QuestionId = dto.Question.TestQuestionId,
            TestName = dto.TestName,
            TestColor = dto.TestColor,
            SpentTimeInSeconds = dto.SpentTimeInSeconds,
            TestId = dto.TestId,
            TotalQuestions = dto.TotalQuestions
        };
    }
}

namespace KramarDev.Quiz.WebAPI.Model;

public class AnswerResponseModel
{
    public QuestionModel NextQuestion { get; set; }

    public TestResultModel TestResult { get; set; }

    public static AnswerResponseModel FromBLL(AnswerResponseDto dto)
    {
        AnswerResponseModel model = new();

        if (dto.NextQuestion != null)
        {
            model.NextQuestion = QuestionModel.FromBLL(dto.NextQuestion);
        }

        if (dto.TestResult != null)
        {
            model.TestResult = TestResultModel.FromBLL(dto.TestResult);
        }

        return model;
    }
}

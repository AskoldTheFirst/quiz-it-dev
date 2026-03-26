
namespace KramarDev.Quiz.DALAbstractions.Dto;

public sealed class AnswerResultDto
{
    public string QuestionText { get; set; }

    public string Answer { get; set; }

    public string CorrectAnswer { get; set; }

    public byte Complexity { get; set; }
}
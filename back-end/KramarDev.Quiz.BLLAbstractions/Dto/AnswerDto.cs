
namespace KramarDev.Quiz.BLLAbstractions.Dto;

public sealed class AnswerDto
{
    public string QuestionText { get; set; }

    public string Answer { get; set; }

    public string CorrectAnswer { get; set; }

    public byte Complexity { get; set; }
}

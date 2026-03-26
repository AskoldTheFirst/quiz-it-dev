namespace KramarDev.Quiz.WebAPI.Model
{
    public class AnswerModel
    {
        public string QuestionText { get; set; }

        public string Answer { get; set; }

        public string CorrectAnswer { get; set; }

        public byte Complexity { get; set; }

        public static AnswerModel FromBLL(AnswerDto dto)
        {
            return new AnswerModel {
                QuestionText = dto.QuestionText,
                Answer = dto.Answer,
                CorrectAnswer = dto.CorrectAnswer,
                Complexity = dto.Complexity,
            };
        }
    }
}

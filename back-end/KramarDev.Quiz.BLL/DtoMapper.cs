using DAL = KramarDev.Quiz.DALAbstractions.Dto;

namespace KramarDev.Quiz.BLL;

static class DtoMapper
{
    public static AnswerDto[] FromDAL(DAL.AnswerResultDto[] dto)
    {
        AnswerDto[] answers = new AnswerDto[dto.Length];
        for(int i = 0; i < dto.Length; ++i)
        {
            answers[i] = new AnswerDto() {
                Answer = dto[i].Answer,
                Complexity = dto[i].Complexity,
                CorrectAnswer = dto[i].CorrectAnswer,
                QuestionText = dto[i].QuestionText,
            };
        }

        return answers;
    }

    public static TechnologyDto[] FromDAL(DAL.TechnologyDto[] dto)
    {
        TechnologyDto[] technologies = new TechnologyDto[dto.Length];
        for (int i = 0; i < technologies.Length; ++i)
        {
            technologies[i] = new TechnologyDto
            {
                Id = dto[i].Id,
                Name = dto[i].Name,
                Description = dto[i].Description,
                DurationInMinutes = dto[i].DurationInMinutes,
                QuestionCount = dto[i].QuestionCount,
            };
        }

        return technologies;
    }

    public static QuestionDto FromDAL(DAL.QuestionDto dto)
    {
        QuestionDto question = new QuestionDto
        {
            Number = dto.Number,
            TestId = dto.TestId,
            QuestionId = dto.QuestionId,
            TestQuestionId = dto.TestQuestionId,
            Text = dto.Text,
            Answer1 = dto.Answer1,
            Answer2 = dto.Answer2,
            Answer3 = dto.Answer3,
            Answer4 = dto.Answer4
        };

        return question;
    }
}

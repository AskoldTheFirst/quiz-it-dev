namespace KramarDev.Quiz.WebAPI;

public static class DtoMapper
{
    public static NextQuestionStateModel FromBLL(NextQuestionStateDto questionDto)
    {
        return new NextQuestionStateModel
        {
            Question = QuestionModel.FromBLL(questionDto.Question),
            QuestionNumber = questionDto.QuestionNumber,
            TotalAmount = questionDto.TotalAmount,
            SecondsLeft = questionDto.SecondsLeft,
            TechnologyName = questionDto.TechnologyName
        };
    }

    public static TestResultModel FromBLL(TestResultDto questionDto)
    {
        return new TestResultModel
        {
            Score = questionDto.Score,
            TimeSpentInSeconds = questionDto.TimeSpentInSeconds,
            QuestionsAmount = questionDto.QuestionsAmount
        };
    }

    public static TechnologyModel FromBLL(TechnologyDto technologyDto)
    {
        return new TechnologyModel
        {
            Id = technologyDto.Id,
            Name = technologyDto.Name,
            Description = technologyDto.Description,
            QuestionCount = technologyDto.QuestionCount,
            DurationInMinute = technologyDto.DurationInMinutes,
            Color = technologyDto.Color,
            Icon = technologyDto.IconName,
        };
    }

    public static TechnologyModel[] FromBLL(TechnologyDto[] technologiesDto)
    {
        TechnologyModel[] technologies = new TechnologyModel[technologiesDto.Length];

        for(int i = 0; i < technologiesDto.Length; i++)
            technologies[i] = FromBLL(technologiesDto[i]);

        return technologies;
    }
}

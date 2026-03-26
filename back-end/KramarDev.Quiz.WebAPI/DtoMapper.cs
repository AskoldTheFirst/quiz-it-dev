namespace KramarDev.Quiz.WebAPI;

public static class DtoMapper
{
    public static TechnologyModel FromBLL(TechnologyDto technologyDto)
    {
        return new TechnologyModel
        {
            Id = technologyDto.Id,
            Name = technologyDto.Name,
            Description = technologyDto.Description,
            QuestionCount = technologyDto.QuestionCount,
            DurationInMinute = technologyDto.DurationInMinutes,
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

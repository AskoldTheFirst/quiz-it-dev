namespace KramarDev.Quiz.WebAPI;

public static class DtoMapper
{
    public static TopicModel FromBLL(TopicDto topicDto)
    {
        return new TopicModel
        {
            Id = topicDto.Id,
            Name = topicDto.Name,
            Description = topicDto.Description,
            QuestionCount = topicDto.QuestionCount,
            DurationInMinute = topicDto.DurationInMinutes,
            ThemeColor = topicDto.ThemeColor,
        };
    }

    public static TopicModel[] FromBLL(TopicDto[] topicsDto)
    {
        TopicModel[] topics = new TopicModel[topicsDto.Length];

        for(int i = 0; i < topicsDto.Length; i++)
            topics[i] = FromBLL(topicsDto[i]);

        return topics;
    }
}

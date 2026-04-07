namespace KramarDev.Quiz.WebAPI.Model;

public sealed class AppStateModel
{
    public TopicModel[] Topics { get; set; }

    public UserModel User { get; set; }
}

public sealed record TopicModel
{
    public int Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public int QuestionCount { get; init; }

    public int DurationInMinute { get; init; }

    public string ThemeColor { get; init; }
}

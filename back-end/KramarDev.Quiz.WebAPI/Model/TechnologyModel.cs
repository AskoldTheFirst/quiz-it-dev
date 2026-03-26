namespace KramarDev.Quiz.WebAPI.Model;

public class TechnologyModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int QuestionCount { get; set; }

    public int DurationInMinute { get; set; }
}

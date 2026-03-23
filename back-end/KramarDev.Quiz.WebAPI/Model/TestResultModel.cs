namespace KramarDev.Quiz.WebAPI.Model;

public sealed class TestResultModel
{
    public float Score { get; set; }

    public int TimeSpentInSeconds { get; set; }

    public int QuestionsAmount { get; set; }
}
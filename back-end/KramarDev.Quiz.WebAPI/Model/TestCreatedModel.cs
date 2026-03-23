using System.Globalization;

namespace KramarDev.Quiz.WebAPI.Model;

public sealed class TestCreatedModel
{
    public int TestId { get; set; }

    public int TotalQuestions { get; set; }

    public int SecondsLeft { get; set; }

    public string TechnologyName { get; set; }

    public string TestColor { get; set; }

    public QuestionModel FirstQuestion { get; set; }

    public static TestCreatedModel FromBL(TestCreatedDto testDto)
    {
        return new TestCreatedModel
        {
            TestId = testDto.TestId,
            TechnologyName = testDto.TechnologyName,
            TestColor = testDto.TestColor,
            TotalQuestions = testDto.QuestionsAmount,
            SecondsLeft = testDto.TestDurationInSeconds,
            FirstQuestion = QuestionModel.FromBLL(testDto.FirstQuestion),
        };
    }
}

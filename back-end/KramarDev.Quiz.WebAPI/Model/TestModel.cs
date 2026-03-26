
namespace KramarDev.Quiz.WebAPI.Model;

public sealed class TestModel
{
    public int TestId { get; set; }

    public string TechnologyName { get; set; }

    public int SecondsLeft { get; set; }

    public int QuestionCount { get; set; }

    public QuestionModel Question { get; set; }

    public static TestModel FromBL(TestDto testDto)
    {
        return new TestModel
        {
            TestId = testDto.TestId,
            TechnologyName = testDto.TechnologyName,
            SecondsLeft = testDto.SecondsLeft,
            QuestionCount = testDto.QuestionCount,
            Question = QuestionModel.FromBLL(testDto.Question),
        };
    }
}

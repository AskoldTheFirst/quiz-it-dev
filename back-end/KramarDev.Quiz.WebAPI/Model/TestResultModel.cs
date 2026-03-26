namespace KramarDev.Quiz.WebAPI.Model
{
    public class TestResultModel
    {
        public string TechnologyName { get; set; }

        public float FinalScore { get; set; }

        public int TotalPoints { get; set; }

        public int EarnedPoints { get; set; }

        public int AnsweredCount { get; set; }

        public AnswerModel[] Answers { get; set; }

        public static TestResultModel FromBLL(TestResultDto dto)
        {
            TestResultModel model = new();

            model.TechnologyName = dto.TechnologyName;
            model.TotalPoints = dto.TotalPoints;
            model.EarnedPoints = dto.EarnedPoints;
            model.FinalScore = dto.FinalScore;
            model.AnsweredCount = dto.AnsweredCount;
            model.Answers = new AnswerModel[dto.Answers.Length];

            for (int i = 0; i < dto.Answers.Length; ++i)
            {
                model.Answers[i] = AnswerModel.FromBLL(dto.Answers[i]);
            }

            return model;
        }
    }
}

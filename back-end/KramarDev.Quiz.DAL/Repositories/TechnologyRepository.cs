namespace KramarDev.Quiz.DAL.Repositories;

public sealed class TechnologyRepository(QuizDbContext dbCtx) : ITechnologyRepository
{
    private readonly QuizDbContext Ctx = dbCtx;

    public async Task<TechnologyDto[]> GetTechnologiesAsync()
    {
        return await (from t in Ctx.Technologies
                        where t.IsActive
                        select new TechnologyDto {
                            Id = t.Id,
                            Name = t.Name,
                            Color = t.Color,
                            Description = t.Description,
                            IconName = t.IconName,
                            QuestionCount = t.QuestionCount,
                            DurationInMinutes = t.DurationInMinutes }).ToArrayAsync();
    }
}

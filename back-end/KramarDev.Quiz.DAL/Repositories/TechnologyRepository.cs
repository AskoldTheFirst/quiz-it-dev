namespace KramarDev.Quiz.DAL.Repositories;

public sealed class TechnologyRepository(QuizDbContext dbCtx) : ITechnologyRepository
{
    private readonly QuizDbContext Ctx = dbCtx;

    public async Task<TechnologyDto[]> GetTechnologiesAsync()
    {
        return await (from t in Ctx.Technologies
                      where t.IsActive
                      select new TechnologyDto
                      {
                          Id = t.Id,
                          Name = t.Name,
                          Description = t.Description,
                          QuestionCount = t.QuestionCount,
                          DurationInMinutes = t.DurationInMinutes
                      }).ToArrayAsync();
    }

    public async Task<TechnologyDto> GetTechnologyByTestIdAsync(int testId)
    {
        return await (from te in Ctx.Technologies
                      join t in Ctx.Tests on te.Id equals t.TechnologyId
                      where t.Id == testId
                      select new TechnologyDto
                      {
                          Id = te.Id,
                          Name = te.Name,
                          Description = te.Description,
                          QuestionCount = te.QuestionCount,
                          DurationInMinutes = te.DurationInMinutes
                      }).SingleAsync();
    }
}

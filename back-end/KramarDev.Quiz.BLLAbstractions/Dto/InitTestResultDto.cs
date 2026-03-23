namespace KramarDev.Quiz.BLLAbstractions.Dto;

public sealed class InitTestResultDto
{
    public int TestId { get; set; }

    public int TotalAmount { get; set; }

    public int SecondsLeft { get; set; }

    public string TechnologyName { get; set; }
}

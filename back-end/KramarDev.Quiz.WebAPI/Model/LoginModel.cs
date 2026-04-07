namespace KramarDev.Quiz.WebAPI.Model;

public sealed record LoginModel
{
    public string Username { get; init; }

    public string Password { get; init; }
}

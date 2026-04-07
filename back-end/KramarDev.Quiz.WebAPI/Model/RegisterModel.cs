namespace KramarDev.Quiz.WebAPI.Model;

public sealed record RegisterModel
{
    public string Username { get; init; }

    public string Password { get; init; }

    public string Email { get; init; }
}

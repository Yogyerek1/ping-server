namespace Ping.Server.Features.Authentication.Contracts;

public sealed class TokenResponse
{
    public string AccessToken { get; init; } = string.Empty;

    public string TokenType { get; init; } = "Bearer";

    public int ExpiresInSeconds { get; init; }
}
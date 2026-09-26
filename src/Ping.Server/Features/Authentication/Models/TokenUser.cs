namespace Ping.Server.Features.Authentication.Models;

public sealed class TokenUser
{
    public Guid UserId { get; init; }

    public string? Email { get; init; }

    public string? UserName { get; init; }

    public IReadOnlyCollection<string> Roles { get; init; } = Array.Empty<string>();
}
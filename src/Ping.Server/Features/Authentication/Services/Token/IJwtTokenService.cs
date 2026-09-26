using System.Security.Claims;
using Ping.Server.Features.Authentication.Models;

namespace Ping.Server.Features.Authentication.Services;

public interface IJwtTokenService
{
    Task<string> GenerateAccessTokenAsync(
        ApplicationUser user,
        CancellationToken cancellationToken = default
    );

    ClaimsPrincipal ValidateToken(string token);

    TokenUser GetTokenUser(string token);
}
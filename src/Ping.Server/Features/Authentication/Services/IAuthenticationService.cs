using Ping.Server.Features.Authentication.Contracts;

namespace Ping.Server.Features.Authentication.Services;

public interface IAuthenticationService
{
    Task<TokenResponse> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default
    );

    Task<TokenResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default
    );
}
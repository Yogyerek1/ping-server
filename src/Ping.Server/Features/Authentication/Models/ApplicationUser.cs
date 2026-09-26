using Microsoft.AspNetCore.Identity;

namespace Ping.Server.Features.Authentication.Models;

public sealed class ApplicationUser : IdentityUser<Guid>
{
}
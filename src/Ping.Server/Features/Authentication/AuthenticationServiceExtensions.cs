using Microsoft.AspNetCore.Identity;
using Ping.Server.Data;
using Ping.Server.Features.Authentication.Models;

namespace Ping.Server.Features.Authentication;

public static class AuthenticationServiceExtensions
{
    public static IServiceCollection AddAuthenticationInfrastructure(
        this IServiceCollection services)
    {
        services.AddDataProtection();
        
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.User.RequireUniqueEmail = true;

            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;

            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.AllowedForNewUsers = true;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<PingDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}
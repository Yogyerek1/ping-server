using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using Ping.Server.Data;

namespace Ping.Server.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration
            .GetSection("CorsSettings:AllowedOrigins")
            .Get<string[]>() ?? Array.Empty<string>();
        
        services.AddCors(options =>
        {
            options.AddPolicy("PingCorsPolicy",
                policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });

        return services;
    }

    public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info.Title = "Ping API";
                document.Info.Version = configuration["Version"] ?? "v0.0.0";
                document.Info.Description = "A self-hosted, decentralized backend for secure, modern communication.";
                return Task.CompletedTask;
            });
        });

        return services;
    }

    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<PingDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        return services;
    }

    public static async Task CheckDatabaseConnectionAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PingDbContext>();
            try
            {
                var canConnect = await db.Database.CanConnectAsync();

                if (canConnect)
                    Console.WriteLine("[INFO] -> Database connection successful.");
                else
                    throw new InvalidOperationException("PostgreSQL database is unreachable. Check connection string and server status.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[FATAL] -> Database connection failed.");
                throw new InvalidOperationException("Failed to connect to the database during startup.", ex);
            }
        }
    }
}
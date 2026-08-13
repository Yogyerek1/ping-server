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
}
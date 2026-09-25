using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System.Net;
using System.Net.Mail;
using Ping.Server.Shared.Services.Email.Options;

namespace Ping.Server.Shared.Services.Email;

public static class EmailServiceExtensions
{
    public static IServiceCollection AddEmailInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var smtpOptions = configuration.GetSection(SmtpOptions.SectionName).Get<SmtpOptions>()
            ?? new SmtpOptions();

        if (string.IsNullOrWhiteSpace(smtpOptions.Host)
            || string.IsNullOrWhiteSpace(smtpOptions.SenderEmail)
            || string.IsNullOrWhiteSpace(smtpOptions.Username)
            || string.IsNullOrWhiteSpace(smtpOptions.Password))
        {
            throw new InvalidOperationException(
                $"SMTP configuration is incomplete. Please define '{SmtpOptions.SectionName}' in appsettings or environment variables.");
        }

        services.Configure<SmtpOptions>(configuration.GetSection(SmtpOptions.SectionName));

        services
            .AddFluentEmail(smtpOptions.SenderEmail, smtpOptions.SenderName)
            .AddRazorRenderer()
            .AddSmtpSender(new SmtpClient(smtpOptions.Host, smtpOptions.Port)
            {
                Credentials = new NetworkCredential(smtpOptions.Username, smtpOptions.Password),
                EnableSsl = smtpOptions.EnableSsl
            });

        services.AddTransient<IEmailService, FluentEmailService>();

        return services;
    }
}
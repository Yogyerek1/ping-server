using FluentEmail.Core;

namespace Ping.Server.Shared.Services.Email;

public class FluentEmailService : IEmailService
{
    private readonly IFluentEmail _fluentEmail;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<FluentEmailService> _logger;

    public FluentEmailService(
        IFluentEmail fluentEmail,
        IWebHostEnvironment env,
        ILogger<FluentEmailService> logger)
    {
        _fluentEmail = fluentEmail;
        _env = env;
        _logger = logger;
    }

    public async Task SendRazorTemplateEmailAsync<TModel>(
        string to,
        string subject,
        string templateName,
        TModel model,
        CancellationToken cancellationToken = default) where TModel : class
    {
        var templatePath = Path.Combine(_env.ContentRootPath, "Shared", "Services", "Email", "Templates", $"{templateName}.cshtml");
        
        if (!File.Exists(templatePath))
            throw new FileNotFoundException($"The Razor email template '{templateName}.cshtml' was not found at {templatePath}");
        
        var response = await _fluentEmail
            .To(to)
            .Subject(subject)
            .UsingTemplateFromFile(templatePath, model)
            .SendAsync(cancellationToken);
        
        if (!response.Successful)
        {
            var errors = string.Join(", ", response.ErrorMessages);
            _logger.LogError("Email sending failed to {to}. Errors: {errors}", to, errors);
            throw new InvalidOperationException($"Failed to send email: {errors}");
        }
    }
}
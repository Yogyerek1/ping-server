namespace Ping.Server.Shared.Services.Email;

public interface IEmailService
{
    Task SendRazorTemplateEmailAsync<TModel>(
        string to,
        string subject,
        string templateName,
        TModel model,
        CancellationToken cancellationToken = default
    ) where TModel : class;
}
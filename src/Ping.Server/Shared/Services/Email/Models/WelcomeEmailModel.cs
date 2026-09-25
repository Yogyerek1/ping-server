namespace Ping.Server.Shared.Services.Email.Models;

public record WelcomeEmailModel(
    string Name,
    string ConfirmUrl,
    List<string> Features
);
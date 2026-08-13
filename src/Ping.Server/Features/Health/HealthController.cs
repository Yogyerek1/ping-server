using Microsoft.AspNetCore.Mvc;

namespace Ping.Server.Features.Health;

[ApiController]
[Route("/health")]
public class HealthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public HealthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet()]
    public IActionResult HealthCheck()
    {
        return Ok(new
        {
            status = "Online",
            timestamp = DateTime.UtcNow,
            version = _configuration["Version"] ?? "v0.0.0"
        });
    }
}
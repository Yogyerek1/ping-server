using Ping.Server.Common.Extensions;
using Ping.Server.Features.Authentication;
using Ping.Server.Shared.Services.Email;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// cors policy
builder.Services.AddCorsPolicy(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddOpenApiDocumentation(builder.Configuration);

builder.Services.AddDatabaseContext(builder.Configuration);

builder.Services.AddAuthenticationInfrastructure();

builder.Services.AddEmailInfrastructure(builder.Configuration);

var app = builder.Build();

await app.CheckDatabaseConnectionAsync();
await app.ApplyMigrationsAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors("PingCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

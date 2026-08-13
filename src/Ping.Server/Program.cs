using Ping.Server.Common.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// cors policy
builder.Services.AddCorsPolicy(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApiDocumentation(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors("PingCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();


using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.RateLimiting;
using MinimalApiBoilerplate.Api.Authentication;
using MinimalApiBoilerplate.Api.Configuration;
using MinimalApiBoilerplate.Api.Middleware;
using Scalar.AspNetCore;
using TickerQ.DependencyInjection;
using Serilog;

namespace MinimalApiBoilerplate.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        builder.Services.AddOpenApi();

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1.0);
            options.AssumeDefaultVersionWhenUnspecified = true;
        });

        builder.ConfigureAppSettings();

        builder.Services.ConfigureRateLimiter();
        builder.Services.ConfigureMongo();
        builder.Services.ConfigureServices();
        builder.Services.ConfigureRepositories();
        builder.Services.ConfigureValidators();
        builder.Services.ConfigureTickerQ();

        builder.ConfigureSerilog();

        var app = builder.Build();

        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(1.0)
            .HasApiVersion(2.0)
            .ReportApiVersions()
            .Build();

        app.AddGlobalExceptionHandler();
        app.UseMiddleware<ApiKeyAuthMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi().WithMetadata(new SkipApiKeyCheckAttribute());
            app.MapScalarApiReference(options => { options.WithTitle("Shorty API"); }).WithMetadata(new SkipApiKeyCheckAttribute());
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseRateLimiter();

        app.ConfigureEndpoints(versionSet);

        app.UseTickerQ();

        app.MapGet("/", () => Results.Ok("API is running")).WithMetadata(new SkipApiKeyCheckAttribute())
           .RequireRateLimiting("fixed");

        app.Run();
    }
}

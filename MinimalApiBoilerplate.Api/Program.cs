
using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.RateLimiting;
using MinimalApiBoilerplate.Api.Authentication;
using MinimalApiBoilerplate.Api.Configuration;
using MinimalApiBoilerplate.Api.Middleware;
using Scalar.AspNetCore;
using TickerQ.DependencyInjection;

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

        // Add rate limiting. TODO: configure values in appsettings. Extract to another class using ext methods like the others
        //TODO name fixed should be  aconstants
        //TODO currently only using it in mapget /, add it to more places
        builder.Services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("fixed", limiterOptions =>
            {
                limiterOptions.PermitLimit = 5;           // 5 requests
                limiterOptions.Window = TimeSpan.FromSeconds(10); // per 10 seconds
                limiterOptions.QueueLimit = 0;
            });
        });

        builder.ConfigureAppSettings();
        builder.Services.ConfigureMongo();
        builder.Services.ConfigureServices();
        builder.Services.ConfigureRepositories();
        builder.Services.ConfigureValidators();
        builder.Services.ConfigureTickerQ();

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

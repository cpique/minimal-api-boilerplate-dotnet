using Asp.Versioning;
using MinimalApiBoilerplate.Api.Authentication;
using MinimalApiBoilerplate.Api.Middleware;
using Scalar.AspNetCore;
using TickerQ.DependencyInjection;

namespace MinimalApiBoilerplate.Api.Configuration;

/// <summary>
/// Intent of these extension methods is to have a thin <see cref="Program"> class
/// <see cref="BaseConfiguration.Configure(WebApplicationBuilder)"> will run before builder.Build()
/// <see cref="BaseConfiguration.Configure(WebApplicationBuilder)"> will run before builder.Build()
/// </summary>
public static class BaseConfiguration
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();
        builder.Services.AddOpenApi();
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1.0);
            options.AssumeDefaultVersionWhenUnspecified = true;
        });

        builder.ConfigureAppSettings();
        builder.ConfigureSerilog();
        builder.Services.ConfigureRateLimiter();
        builder.Services.ConfigureMongo();
        builder.Services.ConfigureServices();
        builder.Services.ConfigureRepositories();
        builder.Services.ConfigureValidators();
        builder.Services.ConfigureTickerQ();
    }

    public static void Configure(this WebApplication app)
    {
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

        app.UseTickerQ();

        app.ConfigureEndpoints();
    }
}

using Asp.Versioning.Conventions;
using MinimalApiBoilerplate.Api.Authentication;
using MinimalApiBoilerplate.Api.Endpoints;

namespace MinimalApiBoilerplate.Api.Configuration;

public static class EndpointsConfiguration
{
    public static void ConfigureEndpoints(this WebApplication app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(1.0)
            .HasApiVersion(2.0)
            .ReportApiVersions()
            .Build();

        ItemsEndpoint.ConfigureEndpoints(app);
        GreetingsEndpoint.ConfigureEndpoints(app, versionSet);

        app.MapGet("/", () => Results.Ok("API is running")).WithMetadata(new SkipApiKeyCheckAttribute())
           .RequireRateLimiting("fixed");
    }
}
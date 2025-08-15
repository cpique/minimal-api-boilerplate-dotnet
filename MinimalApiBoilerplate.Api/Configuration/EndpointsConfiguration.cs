using Asp.Versioning.Builder;
using MinimalApiBoilerplate.Api.Endpoints;

namespace MinimalApiBoilerplate.Api.Configuration;

public static class EndpointsConfiguration
{
    public static void ConfigureEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        ItemsEndpoint.ConfigureEndpoints(app);
        GreetingsEndpoint.ConfigureEndpoints(app, versionSet);
    }
}
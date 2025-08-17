using Asp.Versioning;
using Asp.Versioning.Builder;
using MinimalApiBoilerplate.Api.Authentication;
using MinimalApiBoilerplate.Application.Responses;
using System.Net;

namespace MinimalApiBoilerplate.Api.Endpoints;

public static class GreetingsEndpoint
{
    private const string BaseRoute = "/api/greetings";

    public static void ConfigureEndpoints(WebApplication app, ApiVersionSet versionSet)
    {
        app.MapGet($"{BaseRoute}/hello", HelloWorldV1)
           .WithApiVersionSet(versionSet)
           .HasApiVersion(1)
           .WithMetadata(new SkipApiKeyCheckAttribute());

        app.MapGet($"{BaseRoute}/hello", HelloWorldV2)
           .WithApiVersionSet(versionSet)
           .HasApiVersion(2)
           .WithMetadata(new SkipApiKeyCheckAttribute());
    }

    private static IResult HelloWorldV1(ILogger<Program> logger, HttpContext context)
    {
        var result = DoWork(context.GetRequestedApiVersion());
        logger.LogDebug("Endpoint executed successfully.");
        return result;
    }

    private static IResult HelloWorldV2(ILogger<Program> logger, HttpContext context)
    {
        return DoWork(context.GetRequestedApiVersion());
    }

    private static IResult DoWork(ApiVersion? apiVersion)
    {
        ApiResponse response = new()
        {
            Result = $"Hello world from V{apiVersion!.ToString()}",
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK
        };

        return Results.Ok(response);
    }
}

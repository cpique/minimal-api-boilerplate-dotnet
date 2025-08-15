using Microsoft.Extensions.Options;
using MinimalApiBoilerplate.Api.Authentication;
using MinimalApiBoilerplate.Api.Common;
using MinimalApiBoilerplate.Domain;

namespace MinimalApiBoilerplate.Api.Middleware;

public class ApiKeyAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _apiKey;


    public ApiKeyAuthMiddleware(RequestDelegate next, IOptionsMonitor<AppSettings> appSettingsMonitor)
    {
        _next = next;
        _apiKey = appSettingsMonitor?.CurrentValue?.ApiKey ?? string.Empty;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip if path starts with /tickerq (dashboard or APIs)
        if (context.Request.Path.StartsWithSegments(Constants.TickerQ.DashboardBasePath, StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }

        var endpoint = context.GetEndpoint();

        // Skip if [SkipApiKeyCheck] is present
        if (endpoint?.Metadata.GetMetadata<SkipApiKeyCheckAttribute>() is not null)
        {
            await _next(context);
            return;
        }

        // Recommended to put this in user secrets for local development
        if (!context.Request.Headers.TryGetValue(Constants.Auth.ApiKeyHeaderName, out var clientApiKey) ||
            clientApiKey != _apiKey)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);
    }
}
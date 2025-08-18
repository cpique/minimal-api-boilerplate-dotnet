using Microsoft.AspNetCore.RateLimiting;
using MinimalApiBoilerplate.Infrastructure;

namespace MinimalApiBoilerplate.Api.Configuration;

public static class RateLimiterConfiguration
{
    public static void ConfigureRateLimiter(this IServiceCollection services)
    {
        // Add rate limiting. TODO: configure values in appsettings. Extract to another class using ext methods like the others
        //TODO name fixed should be  aconstants
        //TODO currently only using it in mapget /, add it to more places
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("fixed", limiterOptions =>
            {
                limiterOptions.PermitLimit = 5;           // 5 requests
                limiterOptions.Window = TimeSpan.FromSeconds(10); // per 10 seconds
                limiterOptions.QueueLimit = 0;
            });
        });
    }
}
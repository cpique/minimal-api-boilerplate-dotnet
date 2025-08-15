using MinimalApiBoilerplate.Api.Common;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;

namespace MinimalApiBoilerplate.Api.Configuration;

public static class TickerQConfiguration
{
    public static void ConfigureTickerQ(this IServiceCollection services)
    {
        services.AddTickerQ(options =>
        {
            options.SetMaxConcurrency(Constants.TickerQ.MaxConcurrency);
            options.AddDashboard(basePath: Constants.TickerQ.DashboardBasePath);
            options.AddDashboardBasicAuth();
        });
    }
}

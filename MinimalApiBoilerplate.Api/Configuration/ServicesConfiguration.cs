using MinimalApiBoilerplate.Application.Services;

namespace MinimalApiBoilerplate.Api.Configuration;

public static class ServicesConfiguration
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IItemService, ItemService>();

        services.AddMemoryCache();

        //TODO, create a way to handle cache better
        //services.AddSingleton<ILinkCacheService, InMemoryLinkCacheService>();

        //Background services
        //TODO create an example of background service, use tickerQ, and settings in appsettings
        //services.AddHostedService<ClickProcessorBackgroundService>();
    }
}
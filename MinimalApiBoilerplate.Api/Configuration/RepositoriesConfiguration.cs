using MinimalApiBoilerplate.Infrastructure;

namespace MinimalApiBoilerplate.Api.Configuration;

public static class RepositoriesConfiguration
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IItemRepository, ItemRepository>();
    }
}
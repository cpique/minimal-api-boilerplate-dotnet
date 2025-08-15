using MinimalApiBoilerplate.Infrastructure;

namespace MinimalApiBoilerplate.Api.Configuration;

public static class MongoConfiguration
{
    public static void ConfigureMongo(this IServiceCollection services)
    {
        services.AddSingleton<MongoDbContext>();
        MongoDbConfig.RegisterClassMaps();
    }
}
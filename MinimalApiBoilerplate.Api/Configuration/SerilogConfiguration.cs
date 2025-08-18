using Serilog;

namespace MinimalApiBoilerplate.Api.Configuration;

public static class SerilogConfiguration
{
    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration)
                         .ReadFrom.Services(services);
        });
    }
}

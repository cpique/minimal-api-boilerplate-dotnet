using MinimalApiBoilerplate.Domain;

namespace MinimalApiBoilerplate.Api.Configuration;

public static class AppSettingsConfiguration
{
    public static void ConfigureAppSettings(this WebApplicationBuilder builder)
    {
        // Bind configuration to AppSettings. This is to use IOptionsMonitor
        builder.Services.Configure<AppSettings>(builder.Configuration);
        // Also register AppSettings as a singleton for direct injection
        var appSettings = builder.Configuration.Get<AppSettings>() ?? throw new InvalidOperationException("AppSettings configuration is missing.");
        builder.Services.AddSingleton(appSettings);
    }
}

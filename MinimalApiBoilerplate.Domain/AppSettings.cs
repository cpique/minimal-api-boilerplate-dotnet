namespace MinimalApiBoilerplate.Domain;

public class AppSettings
{
    //Write in read me how to generate it and put it in user-secrets in the project, so it does not live in the appsettings for local development
    public required string ApiKey { get; init; }
    public required MongoDbSettings MongoDb { get; init; }
    public required TickerQBasicAuthSettings TickerQBasicAuth { get; init; }
    public required BackgroundServiceSettings BackgroundService { get; init; }
}

public class TickerQBasicAuthSettings
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}

public class MongoDbSettings
{
    public required string ConnectionString { get; init; }
    public required string DatabaseName { get; init; }
}

//TODO: add a background service with TickerQ
public class BackgroundServiceSettings
{
    public required MyServiceSettings MyService { get; init; }

    public class MyServiceSettings
    {
        public bool IsEnabled { get; init; } = false;
        public int IntervalInMinutes { get; init; }
    }
}

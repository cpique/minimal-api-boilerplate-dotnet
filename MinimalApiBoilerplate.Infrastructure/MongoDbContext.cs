using Microsoft.Extensions.Options;
using MinimalApiBoilerplate.Domain;
using MongoDB.Driver;

namespace MinimalApiBoilerplate.Infrastructure;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptionsMonitor<AppSettings> appSettingsMonitor)
    {
        ArgumentNullException.ThrowIfNull(appSettingsMonitor, nameof(appSettingsMonitor));
        ArgumentNullException.ThrowIfNull(appSettingsMonitor.CurrentValue, nameof(appSettingsMonitor.CurrentValue));

        var appSettings = appSettingsMonitor.CurrentValue;
        ArgumentNullException.ThrowIfNull(appSettings.MongoDb, nameof(appSettings.MongoDb));

        var mongoSettings = appSettings.MongoDb;

        var client = new MongoClient(mongoSettings.ConnectionString);
        _database = client.GetDatabase(mongoSettings.DatabaseName);
    }

    public IMongoCollection<Item> Items => _database.GetCollection<Item>("items");
}

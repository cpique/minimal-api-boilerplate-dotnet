using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MinimalApiBoilerplate.Seeder;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
}

public class Item
{
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Code { get; set; } = default!;
}

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CreatedAt { get; set; } = DateTime.UtcNow.ToString("o");
    public string UpdatedAt { get; set; } = DateTime.UtcNow.ToString("o");
}


public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<Item> Items => _database.GetCollection<Item>("items");
}

internal class Program
{
    static async Task Main(string[] args)
    {
        // Load configuration
        var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .Build();

        var settings = configuration.GetSection("MongoDb").Get<MongoDbSettings>();


        // Setup DI
        ServiceCollection services = new ServiceCollection();
        services.AddSingleton(settings);
        services.AddSingleton<MongoDbContext>();
        
        var provider = services.BuildServiceProvider();

        var context = provider.GetRequiredService<MongoDbContext>();

        var faker = new Faker<Item>()
            .RuleFor(i => i.UserId, f => Guid.NewGuid())
            .RuleFor(i => i.Name, f => f.Commerce.ProductName())
            .RuleFor(i => i.Description, f => f.Commerce.ProductDescription())
            .RuleFor(i => i.Code, f => f.Random.Int(1, 100).ToString());

        var items = faker.Generate(50);

        var areThereItems = await context.Items.Find(_ => true).FirstOrDefaultAsync();

        if(areThereItems != null)
        {
            Console.WriteLine("DB collection has items, skipping...");
        }

        await context.Items.InsertManyAsync(items);
    }
}

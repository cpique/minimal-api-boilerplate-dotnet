using MinimalApiBoilerplate.Domain;
using MongoDB.Driver;

namespace MinimalApiBoilerplate.Infrastructure;

public class ItemRepository : IItemRepository
{
    private readonly IMongoCollection<Item> _collection;
    private readonly MongoDbContext _dbContext;

    public ItemRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
        _collection = _dbContext.Items;
    }

    public async Task<List<Item>> GetAllAsync(string? userId = null)
    {
        var filter = userId == null
            ? Builders<Item>.Filter.Empty // Match all
            : Builders<Item>.Filter.Eq(r => r.UserId.ToString(), userId); // Match specific user

        var results = await _collection.Find(filter).ToListAsync();

        return results;
    }

    public async Task<Item?> GetByIdAsync(Guid id) =>
        await _collection.Find(r => r.Id == id).FirstOrDefaultAsync();

    public async Task<List<Item>> GetByUserIdAsync(Guid userId) =>
        await _collection.Find(r => r.UserId == userId).ToListAsync();

    public async Task AddAsync(Item item) =>
        await _collection.InsertOneAsync(item);

    public async Task<bool> UpdateAsync(Item item) =>
        (await _collection.ReplaceOneAsync(r => r.Id == item.Id, item)).ModifiedCount > 0;

    public async Task<bool> DeleteAsync(Guid id) =>
        (await _collection.DeleteOneAsync(h => h.Id == id)).DeletedCount > 0;
}


public interface IItemRepository
{
    Task<List<Item>> GetAllAsync(string? userId = null);
    Task<Item?> GetByIdAsync(Guid id);
    Task<List<Item>> GetByUserIdAsync(Guid userId);
    Task AddAsync(Item item);
    Task<bool> UpdateAsync(Item item);
    Task<bool> DeleteAsync(Guid id);
}
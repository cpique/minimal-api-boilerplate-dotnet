using MinimalApiBoilerplate.Application.Mappings;
using MinimalApiBoilerplate.Application.Requests;
using MinimalApiBoilerplate.Application.Responses;
using MinimalApiBoilerplate.Infrastructure;

namespace MinimalApiBoilerplate.Application.Services;


public interface IItemService
{
    Task<IReadOnlyList<ItemResponse>> GetAllAsync(string? userId = null);
    Task<ItemResponse?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<ItemResponse>> GetByUserIdAsync(Guid userId);
    Task AddAsync(CreateItemRequest request);
    Task<bool> UpdateAsync(UpdateItemRequest request);
    Task<bool> DeleteAsync(Guid id);
}

public class ItemService : IItemService
{
    private readonly IItemRepository _repository;

    public ItemService(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ItemResponse>> GetAllAsync(string? userId = null)
    {
        var items = await _repository.GetAllAsync(userId);
        var response = items?.Select(l => l.ToResponse()).ToList().AsReadOnly() ?? Enumerable.Empty<ItemResponse>().ToList().AsReadOnly();
        return response;
    }

    public async Task<ItemResponse?> GetByIdAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id);
        return item?.ToResponse();
    }
    public async Task<IReadOnlyList<ItemResponse>> GetByUserIdAsync(Guid userId)
    {
        var items = await _repository.GetByUserIdAsync(userId);
        var response = items?.Select(l => l.ToResponse()).ToList().AsReadOnly() ?? Enumerable.Empty<ItemResponse>().ToList().AsReadOnly();
        return response;
    }

    public async Task AddAsync(CreateItemRequest request)
    {
        var item = request.ToEntity();
        await _repository.AddAsync(item);
    }

    public async Task<bool> UpdateAsync(UpdateItemRequest request)
    {
        var item = request.ToEntity();
        return await _repository.UpdateAsync(item);
    }

    public async Task<bool> DeleteAsync(Guid id) =>
        await _repository.DeleteAsync(id);
}

using MinimalApiBoilerplate.Application.Requests;
using MinimalApiBoilerplate.Application.Responses;
using MinimalApiBoilerplate.Domain;

namespace MinimalApiBoilerplate.Application.Mappings;

public static class ItemMappingExtensions
{
    // Request → Entity  
    public static Item ToEntity(this CreateItemRequest request)
    {
        var entity = new Item
        {
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            Code = request.Code
        };

        return entity;
    }

    public static Item ToEntity(this UpdateItemRequest request)
    {
        var entity = new Item
        {
            Id = request.Id,
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            Code = request.Code
        };

        return entity;
    }

    // Entity → Response
    public static ItemResponse ToResponse(this Item entity)
    {
        var response = new ItemResponse
        {
            Id = entity.Id,
            UserId = entity.UserId,
            Name = entity.Name,
            Description = entity.Description,
            Code = entity.Code ?? string.Empty,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };

        return response;
    }
}
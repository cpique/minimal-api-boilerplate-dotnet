using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MinimalApiBoilerplate.Domain;

public class Item : BaseEntity
{
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Code { get; set; } = default!;
}
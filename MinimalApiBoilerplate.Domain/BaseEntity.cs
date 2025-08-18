using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MinimalApiBoilerplate.Domain;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CreatedAt { get; set; } = DateTime.UtcNow.ToString("o");
    public string UpdatedAt { get; set; } = DateTime.UtcNow.ToString("o");
}

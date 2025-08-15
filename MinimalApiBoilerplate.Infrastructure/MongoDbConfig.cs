using MinimalApiBoilerplate.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace MinimalApiBoilerplate.Infrastructure;

public static class MongoDbConfig
{
    public static void RegisterClassMaps()
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(BaseEntity)))
        {
            BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
            {
                cm.AutoMap(); // maps all properties automatically
                cm.MapIdMember(c => c.Id)
                  .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
            });
        }

        if (!BsonClassMap.IsClassMapRegistered(typeof(Item)))
        {
            BsonClassMap.RegisterClassMap<Item>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.UserId)
                  .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
            });
        }
    }
}
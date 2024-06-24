using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoInfrastructure.Collections;

public class BookErrorCollection
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public int IdTitle { get; set; }
    public string Title { get; set; }
    public string MessageError { set; get; }
    public bool IsReproccessed { set; get; } = false;
    public DateTime CreatedAt { set; get; }
    public DateTime? UpdatedAt { set; get; }
}

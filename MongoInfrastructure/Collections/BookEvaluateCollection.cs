using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoInfrastructure.Collections.Attributes;

namespace MongoInfrastructure.Collections;

public class BookEvaluateCollection
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public int IdTitle { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { set; get; }
    public IEnumerable<MarcCopyAttribute> arrCopy { set; get; }
}

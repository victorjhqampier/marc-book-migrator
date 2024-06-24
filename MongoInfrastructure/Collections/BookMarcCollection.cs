using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoInfrastructure.Collections.Attributes;

namespace MongoInfrastructure.Collections;

public class BookMarcCollection
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public int IdTitle { get; set; }
    public string Dewey { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Edition { get; set; }
    public string Released { get; set; }
    public string Content { get; set; }
    public string Isbn { get; set; }
    public string Physicaldesc { get; set; }
    public string Notes { get; set; }
    public string Topics { get; set; }
    public string Type { get; set; }
    public string Image { get; set; }
    public IEnumerable<MarcAuthorAttribute>? arrAuthor { set; get; }
    public MarcCassifyAttribute? objClassification { set; get; }
    public IEnumerable<MarcPublishAttribute>? arrPublisher { set; get; }
    public MarcSerieAttribute? objSerie { set; get; }
    public IEnumerable<MarcCopyAttribute>? arrCopy { set; get; }
    public DateTime ExportedAt { set; get; }
}

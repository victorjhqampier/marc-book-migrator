
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoInfrastructure.Collections;

public class LoggerCollection
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string? IdPadre { set; get; }
    public string cTipoOperacion { set; get; }
    public DateTime dFechaRequest {  set; get; }
    public string cJsonRequest {set; get; }
    public DateTime? dFechaResponse { set; get; }
    public string? cJsonResponse { set; get; }
    public bool lResponse { set; get; }
}

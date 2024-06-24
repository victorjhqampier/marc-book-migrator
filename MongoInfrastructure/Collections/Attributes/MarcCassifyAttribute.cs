namespace MongoInfrastructure.Collections.Attributes;

public class MarcCassifyAttribute
{
    public int IdClasification { get; set; }
    public int IdTitle { get; set; }
    public string Dewey { get; set; }
    public string Description { get; set; } = null!;
}

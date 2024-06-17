namespace MongoInfrastructure.Collections.Attributes;

public class MarcCassifyAttribute
{
    public int IdClasification { get; set; }
    public int IdTitle { get; set; }
    public string CDewey { get; set; }
    public string CDescription { get; set; } = null!;
}

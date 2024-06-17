namespace Domain.Entities;

public class MarcCassifyEntity
{
    public int IdClasification { get; set; }
    public int IdTitle { get; set; }
    public string? CDewey { get; set; }
    public string CDescription { get; set; } = null!;
}

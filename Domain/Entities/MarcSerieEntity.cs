namespace Domain.Entities;

public  class MarcSerieEntity
{
    public int IdSerial { get; set; }
    public int IdTitle { get; set; }
    public string CNumber { get; set; } = null!;
    public string CTitle { get; set; } = null!;
}

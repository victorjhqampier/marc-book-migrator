namespace Domain.Entities;

public class MarcCopyEntity
{
    public int IdCopy { get; set; }
    public int IdTitle { get; set; }
    public string? CBarcode { get; set; }
    public string? CNotation { get; set; }
    public int? IdDocumentType { get; set; }
    public string? CDocumentType { get; set; }
    public int? IdLocation { get; set; }
    public string? CLocation { get; set; }
    public int? IdSection { get; set; }
    public string? CSection { get; set; }
    public int? IdStatus { get; set; }
    public string? CStatus { get; set; }
}

namespace MongoInfrastructure.Collections.Attributes;

public class MarcCopyAttribute
{
    public int IdCopy { get; set; }
    public int IdTitle { get; set; }
    public string Barcode { get; set; }
    public string Notation { get; set; }
    public int? IdDocumentType { get; set; }
    public string DocumentType { get; set; }
    public int? IdLocation { get; set; }
    public string Location { get; set; }
    public int? IdSection { get; set; }
    public string Section { get; set; }
    public int? IdStatus { get; set; }
    public string Status { get; set; }
}

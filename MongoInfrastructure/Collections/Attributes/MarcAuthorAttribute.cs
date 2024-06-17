namespace MongoInfrastructure.Collections.Attributes;

public class MarcAuthorAttribute
{
    public int IdAuthor { get; set; }
    public int IdTitle { get; set; }
    public string? Role { get; set; }
    public string Surname { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Date { get; set; } = null!;
}

namespace Domain.Entities;

public class MarcAuthorEntity
{
    public int IdAuthor { get; set; }
    public int IdTitle { get; set; }
    public string? CRole { get; set; }
    public string CSurname { get; set; } = null!;
    public string CName { get; set; } = null!;
    public string CDate { get; set; } = null!;
}

namespace Domain.Entities;

public class MarcPublishEntity
{
    public int IdPublisher { get; set; }
    public int IdTitle { get; set; }
    public string CName { get; set; } = null!;
    public string? CPlace { get; set; }
}

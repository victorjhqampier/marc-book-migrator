namespace Domain.Entities;

public class LoggerEntity
{
    public string? loggerId { get; set; }
    public string? parentId { set; get; }
    public string operationType { set; get; }
    public DateTime requestDate { set; get; } = DateTime.Now;
    public string jsonRequest { set; get; }
    public DateTime? responseDate { set; get; }
    public string? jsonResponse { set; get; }
    public bool responseStatus { set; get; } = false;
}
    
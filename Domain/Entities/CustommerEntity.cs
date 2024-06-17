namespace Domain.Entities;

public class CustommerEntity
{
    public int CustomerIdentifier { get; set; }
    public int CompanyIdentifier { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhoneNumber { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int DaysSinceRegistration { get; set; }
}

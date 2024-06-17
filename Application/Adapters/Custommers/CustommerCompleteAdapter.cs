using Domain.Entities;

namespace Application.Adapters.Custommers;

public class CustommerCompleteAdapter
{
    public CustommerEntity CustommerData { get; set; }
    public CompanyEntity? CompanyData { get; set; }
}

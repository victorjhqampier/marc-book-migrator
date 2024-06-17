using Domain.Entities;

namespace Domain.Interfaces;

public interface ICustommerInfrastructure
{
    public CustommerEntity? GetCustommer(int custommerCardIdentifier, string custommerCardNumber);
}

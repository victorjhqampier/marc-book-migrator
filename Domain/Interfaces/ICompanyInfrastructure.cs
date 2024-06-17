using Domain.Entities;

namespace Domain.Interfaces;

public interface ICompanyInfrastructure
{
    public CompanyEntity? GetCompany(int CompanyIdentifier);
}
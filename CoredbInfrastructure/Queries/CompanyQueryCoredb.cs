using CoredbInfrastructure.Collections.Tables;
using Domain.Entities;
using Domain.Interfaces;

namespace CoredbInfrastructure.Queries;

public class CompanyQueryCoredb : ICompanyInfrastructure
{
    private readonly EntityFrameworkContext db;
    // Use Domain Helpers to PreProcess Data

    public CompanyQueryCoredb(EntityFrameworkContext _db) => db = _db;

    public CompanyEntity? GetCompany(int CompanyIdentifier)
    {
        return db.Empresas.Where(x => x.IdEmpresa == CompanyIdentifier)
            .Select(x => new CompanyEntity()
            {
                CompanyIdentifier = x.IdEmpresa,
                CompanyName = x.CNombre
            })
            .FirstOrDefault();
    }
}

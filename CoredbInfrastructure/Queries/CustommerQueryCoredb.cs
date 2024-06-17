using CoredbInfrastructure.Collections.Tables;
using Domain.Entities;
using Domain.Interfaces;

namespace CoredbInfrastructure.Queries;

public class CustommerQueryCoredb : ICustommerInfrastructure
{
    private readonly EntityFrameworkContext db;
    // Use Domain Helpers to PreProcess Data

    public CustommerQueryCoredb(EntityFrameworkContext _db) => this.db = _db;

    public CustommerEntity? GetCustommer(int cardTypeIndetifier, string cardNumer)
    {
        return db.Clientes.Where(x => x.IdTipoDocumento == cardTypeIndetifier && x.CDocumento == cardNumer)
            .Select(x => new CustommerEntity()
            {
                CustomerIdentifier = x.IdCliente,
                CompanyIdentifier = x.IdEmpresa,
                CustomerName = x.CNombre,
                CustomerEmail = x.CCorreo,
                CustomerPhoneNumber = x.CNumeroTelefono,
                RegistrationDate = x.DFechaRegistro,
                DaysSinceRegistration = x.NDiasRegistro
            })
            .FirstOrDefault();
    }
}

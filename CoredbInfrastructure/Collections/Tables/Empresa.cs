using System;
using System.Collections.Generic;

namespace CoredbInfrastructure.Collections.Tables;

public partial class Empresa
{
    public int IdEmpresa { get; set; }

    public string CNombre { get; set; } = null!;

    public DateTime DFechaRegistro { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}

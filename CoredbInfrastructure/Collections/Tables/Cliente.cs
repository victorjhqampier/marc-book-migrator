using System;
using System.Collections.Generic;

namespace CoredbInfrastructure.Collections.Tables;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int IdEmpresa { get; set; }

    public int IdTipoDocumento { get; set; }

    public string CDocumento { get; set; } = null!;

    public string CNombre { get; set; } = null!;

    public string CCorreo { get; set; } = null!;

    public string CNumeroTelefono { get; set; } = null!;

    public int NDiasRegistro { get; set; }

    public DateTime DFechaRegistro { get; set; }

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace ExtractDatabaseInfrastructure.Collections;

public partial class Axclasification
{
    public uint IdClasification { get; set; }

    public uint IdTitle { get; set; }

    public string? CDewey { get; set; }

    public string CDescription { get; set; } = null!;
}

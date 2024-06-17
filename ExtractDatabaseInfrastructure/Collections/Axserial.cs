using System;
using System.Collections.Generic;

namespace ExtractDatabaseInfrastructure.Collections;

public partial class Axserial
{
    public uint IdSerial { get; set; }

    public uint IdTitle { get; set; }

    public string CNumber { get; set; } = null!;

    public string CTitle { get; set; } = null!;
}

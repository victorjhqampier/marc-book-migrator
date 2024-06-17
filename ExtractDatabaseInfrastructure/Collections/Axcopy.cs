using System;
using System.Collections.Generic;

namespace ExtractDatabaseInfrastructure.Collections;

public partial class Axcopy
{
    public uint IdCopy { get; set; }

    public uint IdTitle { get; set; }

    public string? CBarcode { get; set; }

    public string? CNotation { get; set; }

    public uint? IdDocumentType { get; set; }

    public string? CDocumentType { get; set; }

    public ushort? IdLocation { get; set; }

    public string? CLocation { get; set; }

    public ushort? IdSection { get; set; }

    public string? CSection { get; set; }

    public ushort? IdStatus { get; set; }

    public string? CStatus { get; set; }
}

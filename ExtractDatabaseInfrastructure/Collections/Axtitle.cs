using System;
using System.Collections.Generic;

namespace ExtractDatabaseInfrastructure.Collections;

public partial class Axtitle
{
    public uint IdTitle { get; set; }

    public string? CDewey { get; set; }

    public string? CTitle { get; set; }

    public string? CSubtitle { get; set; }

    public string? CEdition { get; set; }

    public string? NReleased { get; set; }

    public string? CContent { get; set; }

    public string? CIsbn { get; set; }

    public string? CPhysicaldesc { get; set; }

    public string? CNotes { get; set; }

    public string? CTopics { get; set; }

    public string CType { get; set; } = null!;

    public string? CImage { get; set; }
}

using System;
using System.Collections.Generic;

namespace ExtractDatabaseInfrastructure.Collections;

public partial class Axauthor
{
    public uint IdAuthor { get; set; }

    public uint IdTitle { get; set; }

    public string? CRole { get; set; }

    public string CSurname { get; set; } = null!;

    public string CName { get; set; } = null!;

    public string CDate { get; set; } = null!;
}

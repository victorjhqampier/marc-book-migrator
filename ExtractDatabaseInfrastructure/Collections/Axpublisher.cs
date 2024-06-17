using System;
using System.Collections.Generic;

namespace ExtractDatabaseInfrastructure.Collections;

public partial class Axpublisher
{
    public uint IdPublisher { get; set; }

    public uint IdTitle { get; set; }

    public string CName { get; set; } = null!;

    public string? CPlace { get; set; }
}

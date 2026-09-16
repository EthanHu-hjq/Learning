using System;
using System.Collections.Generic;

namespace WMS.Entities;

public partial class Cargo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Tag { get; set; }

    public DateTime? InsertDate { get; set; }
}

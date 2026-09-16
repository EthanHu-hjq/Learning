using System;
using System.Collections.Generic;

namespace WMS.Entities;

public partial class Warehouse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Telephone { get; set; }

    public DateTime? InsertDate { get; set; }

    public string? Tag { get; set; }
}

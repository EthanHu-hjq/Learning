using System;
using System.Collections.Generic;

namespace WMS.Entities;

public partial class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Company { get; set; }

    public string? Telephone { get; set; }

    public string? Email { get; set; }

    public string? Tag { get; set; }

    public DateTime? InsertDate { get; set; }
}

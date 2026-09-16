using System;
using System.Collections.Generic;

namespace WMS.Entities;

public partial class Instore
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int InstoreOrderId { get; set; }

    public int CargoId { get; set; }

    public int LocationId { get; set; }

    public int Number { get; set; }

    public double Price { get; set; }

    public string? Tag { get; set; }

    public DateTime? InsertDate { get; set; }
}

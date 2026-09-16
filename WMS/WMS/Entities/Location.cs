using System;
using System.Collections.Generic;

namespace WMS.Entities;

public partial class Location
{
    public int Id { get; set; }

    public int? WarehouseId { get; set; }

    public string? Name { get; set; }

    public DateTime? InsertDate { get; set; }
}

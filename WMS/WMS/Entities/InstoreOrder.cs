using System;
using System.Collections.Generic;

namespace WMS.Entities;

public partial class InstoreOrder
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? WarehouseId { get; set; }

    public int? SupplierId { get; set; }

    public int? MemberId { get; set; }

    public DateTime? InsertDate { get; set; }
}

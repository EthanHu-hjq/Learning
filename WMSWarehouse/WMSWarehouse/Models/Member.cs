using System;
using System.Collections.Generic;

namespace WMSWarehouse.Models;

public partial class Member
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public DateTime? InsertDate { get; set; }
}

using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Province
{
    public int Id { get; set; }

    public string? ProvinceName { get; set; }

    public virtual ICollection<District> Districts { get; } = new List<District>();
}

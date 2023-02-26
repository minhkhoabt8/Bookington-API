using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Province
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProvinceName { get; set; } = null!;

    public virtual ICollection<District> Districts { get; } = new List<District>();
}

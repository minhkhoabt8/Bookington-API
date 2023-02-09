using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class District
{
    public string Id { get; set; } = null!;

    public string ProvinceId { get; set; } = null!;

    public string DistrictName { get; set; } = null!;

    public virtual ICollection<Court> Courts { get; } = new List<Court>();

    public virtual Province Province { get; set; } = null!;
}

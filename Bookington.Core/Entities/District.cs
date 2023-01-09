using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class District
{
    public int Id { get; set; }

    public int? ProvinceId { get; set; }

    public string? DistrictName { get; set; }

    public virtual ICollection<Court> Courts { get; } = new List<Court>();

    public virtual Province? Province { get; set; }
}

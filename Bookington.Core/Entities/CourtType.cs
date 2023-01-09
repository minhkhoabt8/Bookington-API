using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class CourtType
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public virtual ICollection<SubCourt> SubCourts { get; } = new List<SubCourt>();
}

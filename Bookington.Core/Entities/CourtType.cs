using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class CourtType
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public virtual ICollection<SubCourt> SubCourts { get; } = new List<SubCourt>();
}

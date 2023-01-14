using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Slot
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? RefSubCourt { get; set; }

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    public bool? IsActive { get; set; } = false;

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual SubCourt? RefSubCourtNavigation { get; set; }
}

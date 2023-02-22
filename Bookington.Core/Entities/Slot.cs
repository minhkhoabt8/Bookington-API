using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Slot
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefSubCourt { get; set; } = null!;

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public string DaysInSchedule { get; set; } = null!;

    public double Price { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual SubCourt RefSubCourtNavigation { get; set; } = null!;
}

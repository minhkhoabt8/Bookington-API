using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Slot
{
    public string Id { get; set; } = null!;

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public string DaysInSchedule { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual ICollection<SubCourtSlot> SubCourtSlots { get; } = new List<SubCourtSlot>();
}

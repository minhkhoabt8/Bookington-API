﻿using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Slot
{
    public string Id { get; set; } = null!;

    public string? RefSubCourt { get; set; }

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual SubCourt? RefSubCourtNavigation { get; set; }
}
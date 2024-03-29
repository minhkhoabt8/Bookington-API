﻿using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class SubCourt
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string ParentCourtId { get; set; } = null!;

    public string CourtTypeId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual CourtType CourtType { get; set; } = null!;

    public virtual Court ParentCourt { get; set; } = null!;

    public virtual ICollection<SubCourtSlot> SubCourtSlots { get; } = new List<SubCourtSlot>();
}

﻿using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class SubCourt
{
    public string Id { get; set; } = null!;

    public string ParentCourtId { get; set; } = null!;

    public string CourtTypeId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public virtual CourtType CourtType { get; set; } = null!;

    public virtual Court ParentCourt { get; set; } = null!;

    public virtual ICollection<SubCourtSlot> SubCourtSlots { get; } = new List<SubCourtSlot>();
}

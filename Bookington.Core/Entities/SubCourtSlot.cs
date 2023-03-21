using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class SubCourtSlot
{
    public string Id { get; set; } = null!;

    public string RefSubCourt { get; set; } = null!;

    public string RefSlot { get; set; } = null!;

    public double Price { get; set; }

    public bool IsActive { get; set; }

    public virtual Slot RefSlotNavigation { get; set; } = null!;

    public virtual SubCourt RefSubCourtNavigation { get; set; } = null!;
}

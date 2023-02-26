using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class SubCourt
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = null!;

    public string ParentCourtId { get; set; } = null!;

    public string CourtTypeId { get; set; } = null!;

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public int SlotDuration { get; set; }

    public bool IsActive { get; set; } = false;

    public bool IsDeleted { get; set; } = false;

    public virtual CourtType CourtType { get; set; } = null!;

    public virtual Court ParentCourt { get; set; } = null!;

    public virtual ICollection<Slot> Slots { get; } = new List<Slot>();
}

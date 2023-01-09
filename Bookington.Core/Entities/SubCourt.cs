using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class SubCourt
{
    public string Id { get; set; } = null!;

    public string? ParentCourtId { get; set; }

    public int? CourtTypeId { get; set; }

    public DateTime? CreateAt { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual CourtType? CourtType { get; set; }

    public virtual Court? ParentCourt { get; set; }

    public virtual ICollection<Slot> Slots { get; } = new List<Slot>();
}

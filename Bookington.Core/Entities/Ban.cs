using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Ban
{
    public string Id { get; set; } = null!;

    public string? RefAccount { get; set; }

    public string? RefCourt { get; set; }

    public string Reason { get; set; } = null!;

    public int Duration { get; set; }

    public DateTime? BanUntil { get; set; }

    public DateTime CreateAt { get; set; }

    public bool IsAccountBan { get; set; }

    public bool IsCourtBan { get; set; }

    public bool IsActive { get; set; }

    public virtual Account? RefAccountNavigation { get; set; }

    public virtual Court? RefCourtNavigation { get; set; }
}

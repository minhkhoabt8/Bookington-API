using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Ad
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Title { get; set; } = null!;

    public string? RefCourt { get; set; }

    public string RefImage { get; set; } = null!;

    public int PromotionOrder { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string AdLink { get; set; } = null!;

    public bool IsCourtAd { get; set; }

    public bool IsDeleted { get; set; } = false;

    public virtual Court? RefCourtNavigation { get; set; }
}

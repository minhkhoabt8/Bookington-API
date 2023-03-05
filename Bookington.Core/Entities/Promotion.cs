using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Promotion
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Title { get; set; } = null!;

    public string? RefCourt { get; set; }

    public string RefImage { get; set; } = null!;

    public int PromotionOrder { get; set; }

    public DateTime StartTime { get; set; } = DateTime.Now;

    public DateTime EndTime { get; set; }

    public string Link { get; set; } = null!;

    public bool IsCourtPromotion { get; set; }

    public bool IsDeleted { get; set; } = false;

    public virtual Court? RefCourtNavigation { get; set; }
}

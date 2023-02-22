using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class CourtReport
{
    public string Id { get; set; } = null!;

    public string RefCourt { get; set; } = null!;

    public string ReporterId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public virtual Court RefCourtNavigation { get; set; } = null!;

    public virtual Account Reporter { get; set; } = null!;
}

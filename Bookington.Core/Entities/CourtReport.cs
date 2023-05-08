using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class CourtReport
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefCourt { get; set; } = null!;

    public string ReporterId { get; set; } = null!;

    public string? RefResponse { get; set; }

    public string Content { get; set; } = null!;

    public bool IsResponded { get; set; } = false;

    public virtual Court RefCourtNavigation { get; set; } = null!;

    public virtual CourtReportResponse? RefResponseNavigation { get; set; }

    public virtual Account Reporter { get; set; } = null!;
}

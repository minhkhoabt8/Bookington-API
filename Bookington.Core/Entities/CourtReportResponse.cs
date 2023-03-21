using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class CourtReportResponse
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public virtual ICollection<CourtReport> CourtReports { get; } = new List<CourtReport>();
}

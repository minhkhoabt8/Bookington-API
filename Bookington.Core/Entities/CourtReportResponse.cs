using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class CourtReportResponse
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Content { get; set; } = null!;

    public virtual ICollection<CourtReport> CourtReports { get; } = new List<CourtReport>();
}

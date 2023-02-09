using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class ReportType
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? ReportName { get; set; }

    public virtual ICollection<Report> Reports { get; } = new List<Report>();
}

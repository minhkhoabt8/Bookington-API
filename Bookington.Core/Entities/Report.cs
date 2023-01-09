using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Report
{
    public string Id { get; set; } = null!;

    public int? TypeId { get; set; }

    public string? ReporterId { get; set; }

    public string? Content { get; set; }

    public virtual Account? Reporter { get; set; }

    public virtual ReportType? Type { get; set; }
}

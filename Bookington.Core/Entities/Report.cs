using System;
using System.Collections.Generic;

namespace Bokkington_Api.Entities;

public partial class Report
{
    public string Id { get; set; } = null!;

    public int? TypeId { get; set; }

    public string? ReporterId { get; set; }

    public string? Contents { get; set; }

    public double? Rating { get; set; }

    public DateTime? CreateAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual Account? Reporter { get; set; }
}

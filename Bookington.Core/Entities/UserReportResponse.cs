using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class UserReportResponse
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public virtual ICollection<UserReport> UserReports { get; } = new List<UserReport>();
}

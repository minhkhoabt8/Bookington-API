﻿using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class UserReport
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefUser { get; set; } = null!;

    public string ReporterId { get; set; } = null!;

    public string? RefResponse { get; set; }

    public string Content { get; set; } = null!;

    public bool IsResponded { get; set; }

    public virtual UserReportResponse? RefResponseNavigation { get; set; }

    public virtual Account RefUserNavigation { get; set; } = null!;

    public virtual Account Reporter { get; set; } = null!;
}

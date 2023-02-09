using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class UserReport
{
    public string Id { get; set; } = null!;

    public string RefUser { get; set; } = null!;

    public string ReporterId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public virtual Account RefUserNavigation { get; set; } = null!;

    public virtual Account Reporter { get; set; } = null!;
}

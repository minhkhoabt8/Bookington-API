using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class CourtImage
{
    public string Id { get; set; } = null!;

    public string CourtId { get; set; } = null!;

    public string RefImage { get; set; } = null!;

    public virtual Court Court { get; set; } = null!;
}

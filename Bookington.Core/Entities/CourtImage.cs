using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class CourtImage
{
    public string Id { get; set; } = null!;

    public string? CourtId { get; set; }

    public byte[]? ImageBinary { get; set; }

    public virtual Court? Court { get; set; }
}

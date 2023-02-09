using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class CourtImage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string CourtId { get; set; } = null!;

    public byte[]? ImageBinary { get; set; }

    public virtual Court Court { get; set; } = null!;
}

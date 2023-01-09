using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class CourtImage
{
    public int Id { get; set; }

    public string? CourtId { get; set; }

    public byte[]? ImageBinary { get; set; }

    public virtual Court? Court { get; set; }
}

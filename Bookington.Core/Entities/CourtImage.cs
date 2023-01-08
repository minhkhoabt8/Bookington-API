using System;
using System.Collections.Generic;

namespace Bokkington_Api.Entities;

public partial class CourtImage
{
    public int Id { get; set; }

    public string? CourtId { get; set; }

    public byte[]? Image { get; set; }
}

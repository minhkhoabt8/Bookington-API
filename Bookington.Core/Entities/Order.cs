using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Order
{
    public string Id { get; set; } = null!;

    public string? BookingRef { get; set; }

    public DateTime? OrderAt { get; set; }

    public double? Price { get; set; }

    public virtual Booking? BookingRefNavigation { get; set; }
}

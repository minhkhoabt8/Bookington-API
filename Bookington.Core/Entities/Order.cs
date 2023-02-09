using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? TransactionId { get; set; }

    public DateTime OrderAt { get; set; }

    public double TotalPrice { get; set; }

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();
}

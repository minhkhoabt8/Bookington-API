using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string TransactionId { get; set; } = String.Empty;

    public DateTime OrderAt { get; set; }

    public double TotalPrice { get; set; } = 0;

    public bool IsPaid { get; set; } = false;

    public bool IsCanceled { get; set; } = false;

    public bool IsRefunded { get; set; } = false;

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();
}

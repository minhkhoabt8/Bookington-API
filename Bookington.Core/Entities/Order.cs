using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string TransactionId { get; set; } = null!;

    public string VoucherCode { get; set; } = null!;

    public DateTime OrderAt { get; set; }

    public double OriginalPrice { get; set; }

    public double TotalPrice { get; set; }

    public bool IsPaid { get; set; }

    public bool IsCanceled { get; set; }

    public bool IsRefunded { get; set; }

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual TransactionHistory Transaction { get; set; } = null!;

    public virtual Voucher VoucherCodeNavigation { get; set; } = null!;
}

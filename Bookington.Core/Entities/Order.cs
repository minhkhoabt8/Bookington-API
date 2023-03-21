using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Order
{
    public string Id { get; set; } = null!;

    public string? TransactionId { get; set; }

    public string? VoucherCode { get; set; }

    public DateTime OrderAt { get; set; }

    public double OriginalPrice { get; set; }

    public double TotalPrice { get; set; }

    public bool IsPaid { get; set; }

    public bool IsCanceled { get; set; }

    public bool IsRefunded { get; set; }

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual Transaction? Transaction { get; set; }

    public virtual Voucher? VoucherCodeNavigation { get; set; }
}

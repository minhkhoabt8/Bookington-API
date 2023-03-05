﻿using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? TransactionId { get; set; }

    public string? VoucherCode { get; set; }

    public DateTime OrderAt { get; set; } = DateTime.Now;

    public double OriginalPrice { get; set; }

    public double TotalPrice { get; set; }

    public bool IsPaid { get; set; } = false;

    public bool IsCanceled { get; set; } = false;

    public bool IsRefunded { get; set; } = false;

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual TransactionHistory? Transaction { get; set; }

    public virtual Voucher? VoucherCodeNavigation { get; set; }
}

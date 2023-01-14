using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Booking
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? RefSlot { get; set; }

    public string? BookBy { get; set; }

    public string? VoucherCode { get; set; }

    public DateTime? BookAt { get; set; } = DateTime.Now;

    public double? Price { get; set; }

    public double? OriginalPrice { get; set; }

    public bool? IsPaid { get; set; } = false;

    public bool? IsCanceled { get; set; } = false;

    public bool? IsRefunded { get; set; } = false;

    public virtual Account? BookByNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Slot? RefSlotNavigation { get; set; }

    public virtual Voucher? VoucherCodeNavigation { get; set; }
}

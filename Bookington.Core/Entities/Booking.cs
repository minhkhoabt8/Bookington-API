using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Booking
{
    public string Id { get; set; } = null!;

    public string? RefSlot { get; set; }

    public string? BookBy { get; set; }

    public string? VoucherCode { get; set; }

    public DateTime? BookAt { get; set; }

    public double? Price { get; set; }

    public double? OriginalPrice { get; set; }

    public bool? IsPaid { get; set; }

    public bool? IsCanceled { get; set; }

    public bool? IsRefunded { get; set; }

    public virtual Account? BookByNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Slot? RefSlotNavigation { get; set; }

    public virtual Voucher? VoucherCodeNavigation { get; set; }
}

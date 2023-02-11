using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Booking
{
    public string Id { get; set; } = null!;

    public string RefSlot { get; set; } = null!;

    public string RefOrder { get; set; } = null!;

    public string BookBy { get; set; } = null!;

    public string VoucherCode { get; set; } = null!;

    public DateTime BookAt { get; set; }

    public DateTime PlayDate { get; set; }

    public double Price { get; set; }

    public double OriginalPrice { get; set; }

    public virtual Account BookByNavigation { get; set; } = null!;

    public virtual Order RefOrderNavigation { get; set; } = null!;

    public virtual Slot RefSlotNavigation { get; set; } = null!;

    public virtual Voucher VoucherCodeNavigation { get; set; } = null!;
}

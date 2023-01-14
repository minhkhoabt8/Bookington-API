using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Voucher
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? CreateBy { get; set; }

    public string? RefCourt { get; set; }

    public string VoucherCode { get; set; } = null!;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public double? Discount { get; set; }

    public int? Usages { get; set; }

    public int? MaxQuantity { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CreateAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual Account? CreateByNavigation { get; set; }

    public virtual Court? RefCourtNavigation { get; set; }
}

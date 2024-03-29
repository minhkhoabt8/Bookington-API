﻿using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Voucher
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string CreateBy { get; set; } = null!;

    public string RefCourt { get; set; } = null!;

    public string VoucherCode { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public double Discount { get; set; }

    public int Usages { get; set; }

    public int MaxQuantity { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Account CreateByNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Court RefCourtNavigation { get; set; } = null!;
}

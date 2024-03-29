﻿using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Transaction
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefFrom { get; set; } = null!;

    public string RefTo { get; set; } = null!;

    public string? RefMomoTransaction { get; set; }

    public double Amount { get; set; }

    public string Reason { get; set; } = null!;

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Account RefFromNavigation { get; set; } = null!;

    public virtual MomoTransaction? RefMomoTransactionNavigation { get; set; }

    public virtual Account RefToNavigation { get; set; } = null!;
}

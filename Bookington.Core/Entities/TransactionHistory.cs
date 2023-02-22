using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class TransactionHistory
{
    public string Id { get; set; } = null!;

    public string RefFrom { get; set; } = null!;

    public string RefTo { get; set; } = null!;

    public double Amount { get; set; }

    public string Reason { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Account RefFromNavigation { get; set; } = null!;

    public virtual Account RefToNavigation { get; set; } = null!;
}

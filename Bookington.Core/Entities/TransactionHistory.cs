using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class TransactionHistory
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefFrom { get; set; } = null!;

    public string RefTo { get; set; } = null!;

    public double Amount { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public virtual Account RefFromNavigation { get; set; } = null!;

    public virtual Account RefToNavigation { get; set; } = null!;
}

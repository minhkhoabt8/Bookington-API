using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class MomoTransaction
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Content { get; set; } = null!;

    public double Amount { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public bool IsSuccessful { get; set; }

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}

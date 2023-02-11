using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class UserBalance
{
    public string Id { get; set; } = null!;

    public string RefUser { get; set; } = null!;

    public double Balance { get; set; }

    public virtual Account RefUserNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class UserBalance
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefUser { get; set; } = null!;

    public double Balance { get; set; } = 0;

    public virtual Account RefUserNavigation { get; set; } = null!;
}

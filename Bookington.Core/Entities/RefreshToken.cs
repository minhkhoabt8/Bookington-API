using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class RefreshToken
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefAccount { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public DateTime ExpireAt { get; set; }

    public virtual Account RefAccountNavigation { get; set; } = null!;
}

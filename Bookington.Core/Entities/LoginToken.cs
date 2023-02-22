using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class LoginToken
{
    public string Id { get; set; } = null!;

    public string RefAccount { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public DateTime ExpireAt { get; set; }

    public virtual Account RefAccountNavigation { get; set; } = null!;
}

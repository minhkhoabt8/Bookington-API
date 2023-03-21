using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class LoginToken
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefAccount { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public DateTime ExpireAt { get; set; }

    public bool IsRevoked { get; set; }

    public virtual Account RefAccountNavigation { get; set; } = null!;

    //Custom Properties
    public bool IsExpired => DateTime.Now >= ExpireAt;

    public int ExpiresIn => (int)ExpireAt.Subtract(DateTime.Now).TotalSeconds;

    public void Revoke()
    {
        IsRevoked = true;
    }

    public void ReplaceWith(LoginToken replacement)
    {
        RefreshToken = replacement.Token;
        Revoke();
    }
}

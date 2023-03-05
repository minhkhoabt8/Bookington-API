using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class AccountOtp
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefAccount { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string OtpCode { get; set; } = null!;

    public DateTime ExpireAt { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public bool IsConfirmed { get; set; } = false;

    public virtual Account RefAccountNavigation { get; set; } = null!;
}

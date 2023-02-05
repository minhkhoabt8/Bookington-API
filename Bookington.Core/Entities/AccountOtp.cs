using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class AccountOtp
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? Phone { get; set; }

    public string? Otp { get; set; }

    public DateTime? CreateAt { get; set; }

    public bool? IsConfirmed { get; set; }

    public virtual Account? PhoneNavigation { get; set; }
}

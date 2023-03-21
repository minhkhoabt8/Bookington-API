﻿using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class AccountAvatar
{
    public string Id { get; set; } = null!;

    public string RefImage { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}

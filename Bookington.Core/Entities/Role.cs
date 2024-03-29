﻿using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Role
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}

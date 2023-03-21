﻿using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Notification
{
    public string Id { get; set; } = null!;

    public string RefAccount { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public bool IsRead { get; set; }

    public virtual Account RefAccountNavigation { get; set; } = null!;
}

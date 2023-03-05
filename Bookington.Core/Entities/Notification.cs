using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Notification
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefAccount { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public bool IsRead { get; set; } = false;

    public DateTime DeleteAfter { get; set; } = DateTime.Now.AddDays(7); // Set the delete time to 7 days from now

    public virtual Account RefAccountNavigation { get; set; } = null!;
}

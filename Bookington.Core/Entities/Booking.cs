using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Booking
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefSlot { get; set; } = null!;

    public string RefSubCourt { get; set; } = null!;

    public string RefOrder { get; set; } = null!;

    public string BookBy { get; set; } = null!;

    public DateTime BookAt { get; set; } = DateTime.Now;

    public DateTime PlayDate { get; set; }

    public double Price { get; set; }

    public virtual Account BookByNavigation { get; set; } = null!;

    public virtual ICollection<Match> Matches { get; } = new List<Match>();

    public virtual Order RefOrderNavigation { get; set; } = null!;

    public virtual Slot RefSlotNavigation { get; set; } = null!;

    public virtual SubCourt RefSubCourtNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Round
{
    public string Id { get; set; } = null!;

    public string RefMatch { get; set; } = null!;

    public string RefTeam { get; set; } = null!;

    public int RoundNum { get; set; }

    public int Point { get; set; }

    public virtual Match RefMatchNavigation { get; set; } = null!;

    public virtual Team RefTeamNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class MatchTeam
{
    public string Id { get; set; } = null!;

    public string RefMatch { get; set; } = null!;

    public string RefTeam { get; set; } = null!;

    public virtual Match RefMatchNavigation { get; set; } = null!;

    public virtual Team RefTeamNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class TeamPlayer
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefTeam { get; set; } = null!;

    public string RefAccount { get; set; } = null!;

    public virtual Account RefAccountNavigation { get; set; } = null!;

    public virtual Team RefTeamNavigation { get; set; } = null!;
}

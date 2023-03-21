using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Team
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefMatch { get; set; } = null!;

    public string? RefCompetition { get; set; }

    public string Name { get; set; } = null!;

    public string? TeamCode { get; set; }

    public bool IsCompetitionTeam { get; set; }

    public virtual ICollection<MatchTeam> MatchTeams { get; } = new List<MatchTeam>();

    public virtual Competition? RefCompetitionNavigation { get; set; }

    public virtual Match RefMatchNavigation { get; set; } = null!;

    public virtual ICollection<Round> Rounds { get; } = new List<Round>();

    public virtual ICollection<TeamPlayer> TeamPlayers { get; } = new List<TeamPlayer>();
}

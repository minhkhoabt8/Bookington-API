using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Competition
{
    public string Id { get; set; } = null!;

    public string HostBy { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int NumOfTeamsAllowed { get; set; }

    public string CompetitionCode { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime RegisterDeadline { get; set; }

    public bool IsStarted { get; set; }

    public virtual ICollection<CompetitionMatch> CompetitionMatches { get; } = new List<CompetitionMatch>();

    public virtual Account HostByNavigation { get; set; } = null!;

    public virtual ICollection<Team> Teams { get; } = new List<Team>();
}

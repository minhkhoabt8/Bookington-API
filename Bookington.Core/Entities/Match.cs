using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Match
{
    public string Id { get; set; } = null!;

    public string HostBy { get; set; } = null!;

    public string RefBooking { get; set; } = null!;

    public int NumOfPlayersAllowed { get; set; }

    public int NumOfRounds { get; set; }

    public string? MatchCode { get; set; }

    public bool IsPaymentSplitted { get; set; }

    public virtual ICollection<CompetitionMatch> CompetitionMatches { get; } = new List<CompetitionMatch>();

    public virtual Account HostByNavigation { get; set; } = null!;

    public virtual ICollection<MatchTeam> MatchTeams { get; } = new List<MatchTeam>();

    public virtual Booking RefBookingNavigation { get; set; } = null!;

    public virtual ICollection<Round> Rounds { get; } = new List<Round>();

    public virtual ICollection<Team> Teams { get; } = new List<Team>();
}

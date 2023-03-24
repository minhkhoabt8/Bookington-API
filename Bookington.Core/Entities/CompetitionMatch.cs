using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class CompetitionMatch
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefCompetition { get; set; } = null!;

    public string RefMatch { get; set; } = null!;

    public int MatchPosition { get; set; }

    public virtual Competition RefCompetitionNavigation { get; set; } = null!;

    public virtual Match RefMatchNavigation { get; set; } = null!;
}

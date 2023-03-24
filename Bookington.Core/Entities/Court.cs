using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Court
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string OwnerId { get; set; } = null!;

    public string DistrictId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? Description { get; set; }

    public TimeSpan OpenAt { get; set; }

    public TimeSpan CloseAt { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Ad> Ads { get; } = new List<Ad>();

    public virtual ICollection<Ban> Bans { get; } = new List<Ban>();

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<CourtImage> CourtImages { get; } = new List<CourtImage>();

    public virtual ICollection<CourtReport> CourtReports { get; } = new List<CourtReport>();

    public virtual District District { get; set; } = null!;

    public virtual Account Owner { get; set; } = null!;

    public virtual ICollection<SubCourt> SubCourts { get; } = new List<SubCourt>();

    public virtual ICollection<Voucher> Vouchers { get; } = new List<Voucher>();
}

using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Court
{
    public string Id { get; set; } = null!;

    public string? OwnerId { get; set; }

    public int? DistrictId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public TimeSpan? OpenAt { get; set; }

    public TimeSpan? CloseAt { get; set; }

    public DateTime? CreateAt { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<CourtImage> CourtImages { get; } = new List<CourtImage>();

    public virtual District? District { get; set; }

    public virtual Account? Owner { get; set; }

    public virtual ICollection<SubCourt> SubCourts { get; } = new List<SubCourt>();

    public virtual ICollection<Voucher> Vouchers { get; } = new List<Voucher>();
}

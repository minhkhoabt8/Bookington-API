using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookington.Core.Entities;

public partial class Court
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string OwnerId { get; set; }

    public string? DistrictId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }
    [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan OpenAt { get; set; }
    [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan CloseAt { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public bool? IsActive { get; set; } = false;

    public bool? IsDeleted { get; set; } = false;

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<CourtImage> CourtImages { get; } = new List<CourtImage>();

    public virtual District? District { get; set; }

    public virtual Account? Owner { get; set; }

    public virtual ICollection<SubCourt> SubCourts { get; } = new List<SubCourt>();

    public virtual ICollection<Voucher> Vouchers { get; } = new List<Voucher>();
}

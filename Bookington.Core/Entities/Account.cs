using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Account
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RoleId { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public bool IsActive { get; set; }

    public virtual ICollection<AccountOtp> AccountOtps { get; } = new List<AccountOtp>();

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<CourtReport> CourtReports { get; } = new List<CourtReport>();

    public virtual ICollection<Court> Courts { get; } = new List<Court>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserReport> UserReportRefUserNavigations { get; } = new List<UserReport>();

    public virtual ICollection<UserReport> UserReportReporters { get; } = new List<UserReport>();

    public virtual ICollection<Voucher> Vouchers { get; } = new List<Voucher>();
}

using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Account
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public int? RoleId { get; set; } = 1;

    public string Phone { get; set; } = null!;

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public DateTime? CreateAt { get; set; } = DateTime.Now;

    public bool? IsActive { get; set; } = false;

    public virtual ICollection<AccountOtp> AccountOtps { get; } = new List<AccountOtp>();

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<Court> Courts { get; } = new List<Court>();

    public virtual ICollection<Report> Reports { get; } = new List<Report>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Voucher> Vouchers { get; } = new List<Voucher>();
}

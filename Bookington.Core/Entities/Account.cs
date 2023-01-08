using System;
using System.Collections.Generic;

namespace Bokkington_Api.Entities;

public partial class Account
{
    public string Id { get; set; } = null!;

    public int? RoleId { get; set; }

    public string? Phone { get; set; }

    public string? FullName { get; set; }

    public DateTime? CreateAt { get; set; }

    public bool? IsConfirmed { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<Report> Reports { get; } = new List<Report>();

    public virtual Role? Role { get; set; }
}

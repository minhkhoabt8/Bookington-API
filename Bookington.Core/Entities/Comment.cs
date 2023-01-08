using System;
using System.Collections.Generic;

namespace Bokkington_Api.Entities;

public partial class Comment
{
    public string Id { get; set; } = null!;

    public string? CommentWriterId { get; set; }

    public string? RefCourt { get; set; }

    public string? Contents { get; set; }

    public double? Rating { get; set; }

    public DateTime? CreateAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual Account? CommentWriter { get; set; }
}

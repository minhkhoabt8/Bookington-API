using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Comment
{
    public string Id { get; set; } = null!;

    public string? CommentWriterId { get; set; }

    public string? RefCourt { get; set; }

    public string? Content { get; set; }

    public double? Rating { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public bool IsActive { get; set; } = true;

    public virtual Account? CommentWriter { get; set; }

    public virtual Court? RefCourtNavigation { get; set; }
}

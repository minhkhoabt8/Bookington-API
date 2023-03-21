using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class Comment
{
    public string Id { get; set; }= Guid.NewGuid().ToString();

    public string CommentWriterId { get; set; } = null!;

    public string RefCourt { get; set; } = null!;

    public string Content { get; set; } = null!;

    public double Rating { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Account CommentWriter { get; set; } = null!;

    public virtual Court RefCourtNavigation { get; set; } = null!;
}

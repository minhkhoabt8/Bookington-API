using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class ChatRoom
{
    public string Id { get; set; } = null!;

    public string RefOwner { get; set; } = null!;

    public string RefUser { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<ChatMessage> ChatMessages { get; } = new List<ChatMessage>();

    public virtual Account RefOwnerNavigation { get; set; } = null!;

    public virtual Account RefUserNavigation { get; set; } = null!;
}

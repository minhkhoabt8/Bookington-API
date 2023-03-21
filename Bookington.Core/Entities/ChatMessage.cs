using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class ChatMessage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RefChatroom { get; set; } = null!;

    public string RefOwner { get; set; } = null!;

    public string RefUser { get; set; } = null!;

    public DateTime CreateAt { get; set; } = DateTime.Now;

    public int SequenceOrder { get; set; }

    public bool IsDeleted { get; set; } = false;

    public virtual ChatRoom RefChatroomNavigation { get; set; } = null!;

    public virtual Account RefOwnerNavigation { get; set; } = null!;

    public virtual Account RefUserNavigation { get; set; } = null!;
}

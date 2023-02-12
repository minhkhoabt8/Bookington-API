﻿using System;
using System.Collections.Generic;

namespace Bookington.Core.Entities;

public partial class ChatMessage
{
    public string Id { get; set; } = null!;

    public string RefChatroom { get; set; } = null!;

    public string RefOwner { get; set; } = null!;

    public string RefUser { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public int SequenceOrder { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ChatRoom RefChatroomNavigation { get; set; } = null!;

    public virtual Account RefOwnerNavigation { get; set; } = null!;

    public virtual Account RefUserNavigation { get; set; } = null!;
}
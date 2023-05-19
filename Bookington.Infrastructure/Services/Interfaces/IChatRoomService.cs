using Bookington.Infrastructure.DTOs.ChatRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IChatRoomService
    {
        Task<ChatRoomReadDTO> JoinChatRoom(string userId, string ownerId);
    }
}

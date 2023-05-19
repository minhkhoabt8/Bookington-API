using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IChatRoomRepository :
        IGetAllAsync<ChatRoom>, IAddAsync<ChatRoom>,
        IFindAsync<ChatRoom>,
        IDelete<ChatRoom>
    {
        Task<ChatRoom?> FindChatRoomByCustomerIdAndUserId(string customerId, string ownerId);
        Task<IEnumerable<ChatRoom?>> GetAllChatRoomOfUser(string userId);
        Task<IEnumerable<ChatRoom?>> GetAllChatRoomOfOwner(string userId);
    }
    
}

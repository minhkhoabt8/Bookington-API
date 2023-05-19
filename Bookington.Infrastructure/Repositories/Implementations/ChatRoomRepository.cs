using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class ChatRoomRepository : GenericRepository<ChatRoom, BookingtonDbContext>, IChatRoomRepository
    {
        public ChatRoomRepository(BookingtonDbContext context) : base(context)
        {
        }

        public async Task<ChatRoom?> FindChatRoomByCustomerIdAndUserId(string customerId, string ownerId)
        {
            return await _context.ChatRooms.FirstOrDefaultAsync(c => c.RefOwner == ownerId && c.RefUser == customerId && c.IsActive == true);
        }

        public async Task<IEnumerable<ChatRoom?>> GetAllChatRoomOfUser(string userId)
        {
            return _context.ChatRooms.Where(c => c.RefUser == userId && c.IsActive == true);
        }

        public async Task<IEnumerable<ChatRoom?>> GetAllChatRoomOfOwner(string ownerId)
        {
            return _context.ChatRooms.Where(c => c.RefOwner == ownerId && c.IsActive == true);
        }
    }
}

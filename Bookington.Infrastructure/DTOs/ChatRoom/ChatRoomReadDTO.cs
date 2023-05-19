using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.ChatRoom
{
    public class ChatRoomReadDTO
    {
        public string Id { get; set; }

        public string RefOwner { get; set; } 

        public string RefUser { get; set; }

        public bool IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.ChatRoom
{
    public class ChatRoomWriteDTO
    {
        public string RefOwner { get; set; } = null!;

        public string RefUser { get; set; } = null!;

    }
}

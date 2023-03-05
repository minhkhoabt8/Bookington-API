using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Notification
{
    public class NotificationReadDTO
    {
        public string Id { get; set; }

        public string RefAccount { get; set; }

        public string Content { get; set; }

        public DateTime CreateAt { get; set; } 

        public bool IsRead { get; set; }
    }
}

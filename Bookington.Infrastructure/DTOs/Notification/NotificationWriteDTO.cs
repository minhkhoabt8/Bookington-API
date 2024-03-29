﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Notification
{
    public class NotificationWriteDTO
    {
        public string RefAccount { get; set; }

        public string Content { get; set; }

        public bool IsRead { get; set; }

        public int StatusCode { get; set; } = 0;
    }
}

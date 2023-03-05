using Bookington.Infrastructure.DTOs.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Notification
{
    public class NotificationQuerry : PaginatedQuery
    {
        public string UserId { get; set; }
    }
}

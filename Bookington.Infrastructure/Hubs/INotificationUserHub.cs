using Bookington.Infrastructure.DTOs.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Hubs
{
    public interface INotificationUserHub
    {
        Task SendNotification(string userId, NotificationReadDTO notification);
        Task SendNotificationList(string userId, List<NotificationReadDTO> notifications);
        Task SendToUser(string user, string receiverConnectionId, NotificationReadDTO message);
        Task SendNotificationToAll();
    }
}

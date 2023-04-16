using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Notification;
using Bookington.Infrastructure.Hubs;
using Bookington.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Bookington_Api.Hubs
{
    public class NotificationUserHub : Hub, INotificationUserHub
    {
        private readonly IHubContext<NotificationUserHub> _hubContext;

        public NotificationUserHub(IHubContext<NotificationUserHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotification(string userId, NotificationReadDTO notification)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", notification);
            }
        }

        public async Task SendNotificationToAll()
        {
            var msg = "Hello! This Msg is Push To All Client";
            
            await _hubContext.Clients.All.SendAsync("NotificationForAllUser", msg);
            
        }


        public async Task SendNotificationList(string userId, List<NotificationReadDTO> notifications)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                await _hubContext.Clients.User(userId).SendAsync("ReceiveNotifications", notifications);
            }
        }

    }
}

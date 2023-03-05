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
        public async Task SendNotification(string userId, NotificationReadDTO notification)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", notification);
        }

        public async Task SendNotificationList(string userId, List<NotificationReadDTO> notifications)
        {
            await Clients.User(userId).SendAsync("ReceiveNotifications", notifications);
        }

        public async Task SendDiscountVoucherNotificationAsync(string userId, string voucherCode)
        {
            var notification = new NotificationReadDTO
            {
                Content = $"Congratulations! You have received a discount voucher with code: {voucherCode}"
            };

            await _hubContext.SendNotification(userId, notification);
        }


    }
}

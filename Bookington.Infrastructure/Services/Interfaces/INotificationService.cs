using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationReadDTO> CreateNotificationAsync(NotificationWriteDTO dto);
        Task<PaginatedResponse<NotificationReadDTO>> QueryNotificationOfUserAsync(NotificationQuerry querry);
        Task MarkAsReadAsync(List<string> notificationIds);
        Task SendNotificationToAll();
        Task SendNotificationToAUser(string userId);
        Task SendNotificationToAUser(string userId, string connectionId);
    }
}

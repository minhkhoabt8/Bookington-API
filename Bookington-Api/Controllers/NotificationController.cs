using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Notification;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    /// 
    [Route("notifications")]
    [ApiController]
    public class NotificationController
    {

        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Query notifications
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiPaginatedOkResponse<NotificationReadDTO>))]
        public async Task<IActionResult> QueryNotifications([FromQuery] NotificationQuerry query)
        {
            var notifications = await _notificationService.QueryNotificationOfUserAsync(query);

            return ResponseFactory.PaginatedOk(notifications);
        }

        /// <summary>
        /// Create a new notification
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("signUp")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiOkResponse<NotificationReadDTO>))]
        public async Task<IActionResult> CreateAsync(NotificationWriteDTO dto)
        {
            var created = await _notificationService.CreateNotificationAsync(dto);
            return ResponseFactory.Created(created);
        }

        /// <summary>
        /// Mark notifications as read
        /// </summary>
        /// <returns></returns>
        [HttpPut("")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(List<NotificationReadDTO>))]
        public async Task<IActionResult> MarkAsReadAsync(List<NotificationReadDTO> notifications)
        {
            await _notificationService.MarkAsReadAsync(notifications);

            return ResponseFactory.NoContent();
        }

    }
}

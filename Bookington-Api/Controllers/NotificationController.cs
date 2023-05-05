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
        [HttpPut("/markAllAsRead")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(List<NotificationReadDTO>))]
        public async Task<IActionResult> MarkAsReadAsync(List<string> notificationIds)
        {
            await _notificationService.MarkAsReadAsync(notificationIds);

            return ResponseFactory.NoContent();
        }

        /// <summary>
        /// Push notifications to all uer (Test)
        /// </summary>
        /// <returns></returns>
        [HttpGet("/sendAll")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult>PushNotificationsToAllUser()
        {
            await _notificationService.SendNotificationToAll();

            return ResponseFactory.NoContent();
        }

        /// <summary>
        /// Push notification to A User With userId and connectionId (Test)
        /// </summary>
        /// <returns></returns>
        [HttpGet("userID={userId}/connectionId={connectionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> SendNotificationToUserWithConnectionId(string userId, string connectionId)
        {

            await _notificationService.SendNotificationToAUser(userId, connectionId);
            return ResponseFactory.NoContent();
        }

        /// <summary>
        /// Pushy notifications to A User (Test)
        /// </summary>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> SendNotificationToUser(string userId)
        {

            await _notificationService.SendNotificationToAUser(userId);
            return ResponseFactory.NoContent();
        }


    }
}

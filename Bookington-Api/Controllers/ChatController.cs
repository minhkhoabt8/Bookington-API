using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.Hubs;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    /// <summary>
    /// Chat Controller
    /// </summary>
    /// 
    [Route("chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IChatHub _chatHub;

        /// <summary>        
        /// </summary>
        public ChatController(IChatRoomService chatRoomService, IChatHub chatHub)
        {
            _chatRoomService = chatRoomService;
            _chatHub = chatHub;
        }

        /// <summary>
        /// Join A Chat Room
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[RoleAuthorize(AccountRole.admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> JoinChatRoom(string userId, string ownerId)
        {
            var chatRooms = await _chatRoomService.JoinChatRoom(userId, ownerId);

            return ResponseFactory.Ok(chatRooms);
        }

        /// <summary>
        /// Test Send Message To ChatRoom
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("sendToRoom")]
        public async Task<IActionResult> SendMessageToChatRoom(string chatRoom, string userid, string message)
        {
            await _chatHub.SendMessageToGroup(chatRoom, userid, message);
            return Ok();
        }

        /// <summary>
        /// Test Send Message To ChatRoom
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("JoinRoom")]
        public async Task<IActionResult> SendMessageToChatRoom(string roomId)
        {
            await _chatHub.AddToGroup(roomId);
            return Ok();
        }
    }
}

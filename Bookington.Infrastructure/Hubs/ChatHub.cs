using Bookington_Api.Hubs;
using Microsoft.AspNetCore.SignalR;
using Bookington.Infrastructure.DTOs.Notification;
using Bookington.Infrastructure.Hubs;
using Newtonsoft.Json;

namespace Bookington.Infrastructure.Hubs
{
    public class ChatHub : Hub, IChatHub
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatHub(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task AddToGroup(string groupName)
        {
            var conn = GetConnectionId();
            await _hubContext.Groups.AddToGroupAsync(conn, groupName);
            System.Diagnostics.Debug.WriteLine("Connection ID: " + conn + "GroupName: " + groupName);
        }

        public async Task SendMessageToGroup(string groupName, string senderId, string message)
        {
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveMessage", senderId, message);
        }

        public string GetConnectionId() => Context.ConnectionId;
    }
}

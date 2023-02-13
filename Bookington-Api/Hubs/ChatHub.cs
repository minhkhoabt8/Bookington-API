using Microsoft.AspNetCore.SignalR;

namespace Bookington_Api.Hubs
{
    public class ChatHub : Hub
    {
        [HubMethodName("SendMessage")]
        public async Task BroadcastToConnection(string message, string connectionId)
            => await Clients.Client(connectionId).SendAsync("broadcasttoclient", message);

        public string GetConnectionId() => Context.ConnectionId;
    }
}

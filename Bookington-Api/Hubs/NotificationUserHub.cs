using Bookington.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Bookington_Api.Hubs
{
    public class NotificationUserHub : Hub
    {
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly IUserContextService _userContextService;

        public NotificationUserHub(IUserConnectionManager userConnectionManager, IUserContextService userContextService)
        {
            _userConnectionManager = userConnectionManager;
            _userContextService = userContextService;
        }

        public string GetConnectionId()
        {
            var userId = _userContextService.AccountID;
            _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId); return Context.ConnectionId;
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            //get the connectionId
            var connectionId = Context.ConnectionId;
            _userConnectionManager.RemoveUserConnection(connectionId);
            var value = await Task.FromResult(0);
        }
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage ", GetConnectionId(), message);
        }
    }
}

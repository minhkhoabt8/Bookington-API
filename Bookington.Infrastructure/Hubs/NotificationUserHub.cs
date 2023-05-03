
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Notification;
using Bookington.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Bookington_Api.Hubs
{
    public class NotificationUserHub : Hub, INotificationUserHub
    {
        private readonly IHubContext<NotificationUserHub> _hubContext;

        public NotificationUserHub(IHubContext<NotificationUserHub> hubContext, Dictionary<string, string> userConnectionMap)
        {
            _hubContext = hubContext;
            _userConnectionMap = userConnectionMap;
        }

        private readonly Dictionary<string, string> _userConnectionMap;


        /// <summary>
        /// Map Conenction Id And user Id  
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="connectionId"></param>

        public void RegisterUserConnection(string userId)
        {
            var conn = GetConnectionId();

            lock (_userConnectionMap)
            {
                if (!_userConnectionMap.ContainsKey(userId))
                {
                    
                    _userConnectionMap.Add(userId, conn);
                }
                else
                {
                    _userConnectionMap[userId] = conn;
                }

               
            }
        }

        /// <summary>
        /// sends a notification to a specific user by checking if their user ID
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public async Task SendNotification(string userid, NotificationReadDTO notification)
        {
            if (_userConnectionMap.ContainsKey(userid))
            {
                string connectionId = _userConnectionMap[userid];
                string messageJson = JsonConvert.SerializeObject(notification);
                System.Diagnostics.Debug.WriteLine("Connection ID: " + connectionId + "UserId: " + userid);
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", userid, messageJson);
            }
        }

        /// <summary>
        /// Sends a message to a specific user identified by their connection ID
        /// </summary>
        /// <param name="user"></param>
        /// <param name="receiverConnectionId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendToUser(string user, string receiverConnectionId, NotificationReadDTO message)
        {
            string messageJson = JsonConvert.SerializeObject(message);
            await _hubContext.Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", user, messageJson);
        }


        public async Task SendNotificationList(string userId, List<NotificationReadDTO> notifications)
        {
            if (_userConnectionMap.ContainsKey(userId))
            {
                string connectionId = _userConnectionMap[userId];
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotifications", notifications);
            }
        }


        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = _userConnectionMap.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (!string.IsNullOrEmpty(user))
            {
                _userConnectionMap.Remove(user);
                System.Diagnostics.Debug.WriteLine($"User with ID {user} disconnected and was removed from the connection map.");
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendNotificationToAll()
        {
            var msg = "Hello! This Msg is Push To All Client";

            await _hubContext.Clients.All.SendAsync("NotificationForAllUser", msg);

        }


        public string GetConnectionId() => Context.ConnectionId;

        //------------------------------------------------------------------------------------------------------------

        public async Task SendNotificationOld(string userId, NotificationReadDTO notification)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", notification);
            }
        }

        //-----------------------------------------------------------------------------

    }
}

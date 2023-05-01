﻿
using Bookington.Infrastructure.DTOs.Notification;
using Bookington.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace Bookington_Api.Hubs
{
    public class NotificationUserHub : Hub, INotificationUserHub
    {
        private readonly IHubContext<NotificationUserHub> _hubContext;

        public NotificationUserHub(IHubContext<NotificationUserHub> hubContext)
        {
            _hubContext = hubContext;
        }

        private readonly Dictionary<string, string> _userConnectionMap = new();


        /// <summary>
        /// Map Conenction Id And user Id  
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="connectionId"></param>

        public void RegisterUserConnection(string userId, string connectionId)
        {
            lock (_userConnectionMap)
            {
                if (!_userConnectionMap.ContainsKey(userId))
                {
                    _userConnectionMap.Add(userId, connectionId);
                }
                else
                {
                    _userConnectionMap[userId] = connectionId;
                }

                System.Diagnostics.Debug.WriteLine("User Connection Map:");

                foreach (var pair in _userConnectionMap)
                {
                    System.Diagnostics.Debug.WriteLine($"User ID: {pair.Key}, Connection ID: {pair.Value}");
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
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", userid, notification);
            }
        }

        /// <summary>
        /// Sends a message to a specific user identified by their connection ID
        /// </summary>
        /// <param name="user"></param>
        /// <param name="receiverConnectionId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendToUser(string user, string receiverConnectionId, string message)
        {
            await _hubContext.Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", user, message);
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

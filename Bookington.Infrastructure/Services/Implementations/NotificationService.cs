using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Notification;
using Bookington.Infrastructure.Hubs;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly INotificationUserHub _hubContext;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, INotificationUserHub hubContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
            _hubContext = hubContext;
        }

        public async Task<NotificationReadDTO> CreateNotificationAsync(NotificationWriteDTO dto)
        {
            var notification = _mapper.Map<Notification>(dto);

            await _unitOfWork.NotificationRepository.AddAsync(notification);

            await _unitOfWork.CommitAsync();

            //call the hub to send notification to specific user by userId


            if (!string.IsNullOrEmpty(dto.RefAccount))
            {
                var message = _mapper.Map<NotificationReadDTO>(notification);

                message.Content = dto.Content;

               await _hubContext.SendNotification(dto.RefAccount, message);
            }

            return _mapper.Map<NotificationReadDTO>(notification);
        }

        public async Task MarkAsReadAsync(List<NotificationReadDTO> notifications)
        {
            foreach (var notification in notifications)
            {
                var noti = await _unitOfWork.NotificationRepository.FindAsync(notification.Id);

                if (noti != null)
                {

                    noti.IsRead = true;

                    _unitOfWork.NotificationRepository.Update(noti);

                }
            }
            await _unitOfWork.CommitAsync();
        }

        public async Task<PaginatedResponse<NotificationReadDTO>> QueryNotificationOfUserAsync(NotificationQuerry query)
        {
            var currUserId = _userContextService.AccountID.ToString();

            if (query.UserId != currUserId) throw new ForbiddenException();
            
            //get notification from db
            var notifications = await _unitOfWork.NotificationRepository.QueryAsync(query);

            var notis = _mapper.Map<IEnumerable<NotificationReadDTO>>(notifications).ToList();

            //add notification to list and send through signalR
            await _hubContext.SendNotificationList(currUserId, notis);

            return PaginatedResponse<NotificationReadDTO>.FromEnumerableWithMapping(
                notis, query, _mapper);
        }



        public async Task SendNotificationToAll()
        {
            await _hubContext.SendNotificationToAll();
        }

        public async Task SendNotificationToAUser(string userId)
        {
            
            var noti = new NotificationReadDTO
            {
                Content = "This should send to user: " + userId,
                CreateAt = DateTime.Now,
                Id = Guid.NewGuid().ToString(),   
                IsRead = true,
                RefAccount = userId,
            };
            await _hubContext.SendNotification(userId, noti);
        }


        public async Task SendNotificationToAUser(string userId, string connectionId)
        {

            var noti = new NotificationReadDTO
            {
                Content = "This should send to user: " + userId,
                CreateAt = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                IsRead = true,
                RefAccount = userId,
            };
            await _hubContext.SendToUser(userId, connectionId, noti);
        }

    }
}

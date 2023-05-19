using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.ChatRoom;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChatRoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //JoinChatRoom ownerId, userId

        public async Task<ChatRoomReadDTO> JoinChatRoom(string userId, string ownerId)
        {
            //FindChatRoom
            var existChatRoom = await _unitOfWork.ChatRoomRepository.FindChatRoomByCustomerIdAndUserId(userId, ownerId);

            if(existChatRoom == null)
            {
                //Create new Chat Room and return 
                var chatRoom = new ChatRoom()
                {
                    RefOwner= ownerId,
                    RefUser= userId,
                    IsActive= true,
                };
                await _unitOfWork.ChatRoomRepository.AddAsync(chatRoom);

                await _unitOfWork.CommitAsync();

                return _mapper.Map<ChatRoomReadDTO>(chatRoom);
            }

            return _mapper.Map<ChatRoomReadDTO>(existChatRoom);
        }


        public async Task<IEnumerable<ChatRoomReadDTO>> GetAllChatRoomOfUser(string userId)
        {
            var chatRoom = _unitOfWork.ChatRoomRepository.GetAllChatRoomOfUser(userId);
            return _mapper.Map<IEnumerable<ChatRoomReadDTO>>(chatRoom);
        }

        public async Task<IEnumerable<ChatRoomReadDTO>> GetAllChatRoomOfOwner(string ownerId)
        {
            var chatRoom = _unitOfWork.ChatRoomRepository.GetAllChatRoomOfOwner(ownerId);
            return _mapper.Map<IEnumerable<ChatRoomReadDTO>>(chatRoom);
        }
    }
}

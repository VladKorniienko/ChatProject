using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Application.Interfaces;
using ChatApp.Domain.Entities;

namespace ChatApp.Application.Services
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatRoomService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ChatRoom> CreateRoomAsync(string roomName, User creator)
        {
            var chatRoom = new ChatRoom(roomName, creator);

            await _unitOfWork.ChatRoomRepository.AddAsync(chatRoom);
            await _unitOfWork.SaveChangesAsync();

            return chatRoom;
        }

        public async Task AddUserToRoomAsync(string roomId, User user)
        {
            var chatRoom = await _unitOfWork.ChatRoomRepository.GetByIdAsync(roomId);
            if (chatRoom != null)
            {
                await _unitOfWork.ChatRoomRepository.AddUserToRoomAsync(chatRoom, user);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Chat room not found.");
            }
        }

        public async Task RemoveUserFromRoomAsync(string roomId, User user)
        {
            var chatRoom = await _unitOfWork.ChatRoomRepository.GetByIdAsync(roomId);
            if (chatRoom != null)
            {
                await _unitOfWork.ChatRoomRepository.RemoveUserFromRoomAsync(chatRoom, user);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Chat room not found.");
            }
        }

        public async Task<IEnumerable<ChatRoom>> GetAllRoomsAsync()
        {
            return await _unitOfWork.ChatRoomRepository.GetAllAsync();
        }

        public async Task<ChatRoom> GetRoomAsync(string roomId)
        {
            var chatRoom = await _unitOfWork.ChatRoomRepository.GetByIdAsync(roomId);
            if (chatRoom == null)
            {
                throw new KeyNotFoundException("Chat room not found.");
            }
            return chatRoom;
        }
    }

}

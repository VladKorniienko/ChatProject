using ChatApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces
{
    public interface IChatRoomService
    {
        Task<ChatRoom> CreateRoomAsync(string roomName, User creator);
        Task AddUserToRoomAsync(string roomId, User user);
        Task RemoveUserFromRoomAsync(string roomId, User user);
        Task<IEnumerable<ChatRoom>> GetAllRoomsAsync();
        Task<ChatRoom> GetRoomAsync(string roomId);
    }
}

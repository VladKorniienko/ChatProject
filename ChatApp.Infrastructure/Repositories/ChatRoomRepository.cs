using ChatApp.Application.Repositories;
using ChatApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChatApp.Infrastructure.Data;

namespace ChatApp.Infrastructure.Repositories
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        protected readonly ChatContext _dbContext;
        public ChatRoomRepository(ChatContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<ChatRoom> GetByIdAsync(string roomId)
        {
            return await _dbContext.ChatRooms
                 .Include(c => c.Users)
                 .Include(c => c.Messages)
                 .FirstOrDefaultAsync(c => c.Id == roomId);
        }
        public async Task<IEnumerable<ChatRoom>> GetAllAsync()
        {
            return await _dbContext.ChatRooms
                .ToListAsync();
        }

        public async Task AddAsync(ChatRoom chatRoom)
        {
            await _dbContext.ChatRooms.AddAsync(chatRoom);
        }

        public async Task UpdateAsync(ChatRoom chatRoom)
        {
             _dbContext.ChatRooms.Update(chatRoom);
        }

        public async Task DeleteAsync(string id)
        {
            var chatRoom = await GetByIdAsync(id);
            if (chatRoom != null)
            {
                _dbContext.ChatRooms.Remove(chatRoom);
            }
        }

        public async Task<IEnumerable<Message>> GetRoomAsync(string chatRoomId)
        {
            var chatRoom = await _dbContext.ChatRooms
                .Include(cr=>cr.Users)
                .Include(cr => cr.Messages)
                .FirstOrDefaultAsync(cr => cr.Id == chatRoomId);

            return chatRoom?.Messages ?? new List<Message>();
        }

        public async Task AddUserToRoomAsync(ChatRoom chatRoom, User user)
        {
            if (!chatRoom.Users.Contains(user))
            {
                chatRoom.AddUser(user);
                _dbContext.ChatRooms.Update(chatRoom);
            }
        }

        public async Task RemoveUserFromRoomAsync(ChatRoom chatRoom, User user)
        {
            if (chatRoom.Users.Contains(user))
            {
                chatRoom.RemoveUser(user);
                _dbContext.ChatRooms.Update(chatRoom);
            }
        }
    }
}


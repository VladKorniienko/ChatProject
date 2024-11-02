using ChatApp.Domain.Entities;


namespace ChatApp.Application.Repositories
{
    public interface IChatRoomRepository
    {
        Task<ChatRoom> GetByIdAsync(string chatRoomId);
        Task<IEnumerable<ChatRoom>> GetAllAsync();
        Task AddAsync(ChatRoom chatRoom);
        Task UpdateAsync(ChatRoom chatRoom);
        Task DeleteAsync(string id);
        Task<IEnumerable<Message>> GetRoomAsync(string chatRoomId);
        Task AddUserToRoomAsync(ChatRoom chatRoom, User user);
        Task RemoveUserFromRoomAsync(ChatRoom chatRoom, User user);
    }
}

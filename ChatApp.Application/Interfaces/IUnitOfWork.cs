using ChatApp.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        IChatRoomRepository ChatRoomRepository { get; }
        //IMessageRepository MessageRepository { get; }
        IUserRepository UserRepository { get; }
    }
}

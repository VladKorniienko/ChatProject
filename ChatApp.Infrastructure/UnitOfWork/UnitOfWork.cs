using ChatApp.Application.Interfaces;
using ChatApp.Application.Repositories;
using ChatApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ChatContext _dbContext;
        public IChatRoomRepository ChatRoomRepository { get; }
        //public IMessageRepository MessageRepository { get; }
        public IUserRepository UserRepository { get; }

        public UnitOfWork(ChatContext context,
                          IChatRoomRepository chatRoomRepository,
                          //IMessageRepository messageRepository,
                          IUserRepository userRepository)
        {
            _dbContext = context;
            ChatRoomRepository = chatRoomRepository;
            //MessageRepository = messageRepository;
            UserRepository = userRepository;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}

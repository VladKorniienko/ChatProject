using ChatApp.Application.Interfaces;
using ChatApp.Application.Repositories;
using ChatApp.Infrastructure.Data;
using ChatApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            var connectionString = configuration.GetConnectionString("SqlServer");
            services.AddDbContext<ChatContext>(options =>
                     options.UseSqlServer(connectionString));
        }
    }
}

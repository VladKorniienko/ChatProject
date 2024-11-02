using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.UserName)
                .IsRequired();
            builder.HasIndex(u => u.UserName)
                .IsUnique();
            builder.Property(u => u.Email)
                .IsRequired();
            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}

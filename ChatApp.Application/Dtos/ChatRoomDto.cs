using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Dtos
{
    public class ChatRoomDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<UserDto> Users { get; set; } = new();
        public List<MessageDto> Messages { get; set; } = new();
    }
}

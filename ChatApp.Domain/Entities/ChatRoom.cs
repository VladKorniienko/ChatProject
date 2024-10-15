using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Entities
{
    public class ChatRoom
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}

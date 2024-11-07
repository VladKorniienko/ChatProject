using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Dtos
{
    public class MessageDto
    {
        public string Id { get;  set; }
        public string Content { get;  set; }
        public DateTime TimeSent { get;  set; }
        public string UserId { get;  set; }
    }
}

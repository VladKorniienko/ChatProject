using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace ChatApp.API.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string roomId)
        {
            await Clients.Group(roomId).SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }
    }
}

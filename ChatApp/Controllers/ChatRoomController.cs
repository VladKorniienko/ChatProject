using ChatApp.Application.Interfaces;
using ChatApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;

        public ChatRoomController(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        // GET: api/ChatRoom
        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var chatRooms = await _chatRoomService.GetAllRoomsAsync();
            return Ok(chatRooms);
        }

        // GET: api/ChatRoom/{roomId}
        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoom(string roomId)
        {
            try
            {
                var chatRoom = await _chatRoomService.GetRoomAsync(roomId);
                return Ok(chatRoom);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Chat room not found.");
            }
        }

        // POST: api/ChatRoom
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.RoomName) || request.Creator == null)
                return BadRequest("Invalid request data.");

            var chatRoom = await _chatRoomService.CreateRoomAsync(request.RoomName, request.Creator);
            return CreatedAtAction(nameof(GetRoom), new { roomId = chatRoom.Id }, chatRoom);
        }

        // POST: api/ChatRoom/{roomId}/addUser
        [HttpPost("{roomId}/addUser")]
        public async Task<IActionResult> AddUserToRoom(string roomId, [FromBody] User user)
        {
            try
            {
                await _chatRoomService.AddUserToRoomAsync(roomId, user);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Chat room not found.");
            }
        }

        // DELETE: api/ChatRoom/{roomId}/removeUser
        [HttpDelete("{roomId}/removeUser")]
        public async Task<IActionResult> RemoveUserFromRoom(string roomId, [FromBody] User user)
        {
            try
            {
                await _chatRoomService.RemoveUserFromRoomAsync(roomId, user);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Chat room not found.");
            }
        }
    }

    public class CreateRoomRequest
    {
        public string RoomName { get; set; }
        public User Creator { get; set; }
    }
}


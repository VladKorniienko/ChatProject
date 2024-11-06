using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Entities
{
    public class ChatRoom
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public readonly List<User> _users = new();
        public readonly List<Message> _messages = new();
        public IReadOnlyCollection<User> Users => _users.AsReadOnly();
        public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

        public ChatRoom() { }

        public ChatRoom(string name, User creator)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Room name cannot be empty.");

            if (creator == null)
                throw new ArgumentNullException(nameof(creator), "Creator cannot be null.");

            Id = Guid.NewGuid().ToString();
            Name = name;
            _users.Add(creator);
        }

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null.");

            if (_users.Contains(user))
                throw new InvalidOperationException("User is already a member of the room.");

            _users.Add(user);
        }

        public void RemoveUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null.");

            if (!_users.Contains(user))
                throw new InvalidOperationException("User is not a member of the room.");

            _users.Remove(user);
        }

        public void SendMessage(User user, string content)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content cannot be null or empty.", nameof(content));
            if (!_users.Contains(user))
                throw new InvalidOperationException("User must be a member of the room to send messages.");

            var message = new Message(user.Id, content, this);
            _messages.Add(message);
        }
    }
}

using ChatApp.Domain.Entities;
using System;

namespace ChatApp.Tests
{
    public class ChatRoomEntityTests
    {
        [Fact]
        public void Constructor_ShouldThrowException_WhenRoomNameIsNullOrWhitespace()
        {
            // Arrange
            string roomName = null;
            var creator = new User { Id = "1", UserName = "TestUser" };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new ChatRoom(roomName, creator));
            Assert.Equal("Room name cannot be empty.", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenCreatorIsNull()
        {
            // Arrange
            string roomName = "Valid Room";

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new ChatRoom(roomName, null));
            Assert.Contains("Creator cannot be null.", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldSetProperties_WhenValidDataIsProvided()
        {
            // Arrange
            var roomName = "Test Room";
            var creator = new User { Id = "1", UserName = "TestUser" };

            // Act
            var chatRoom = new ChatRoom(roomName, creator);

            // Assert
            Assert.Equal(roomName, chatRoom.Name);
            Assert.Single(chatRoom.Users);
            Assert.Contains(creator, chatRoom.Users);
        }

        [Fact]
        public void AddUser_ShouldAddUser_WhenUserIsValid()
        {
            // Arrange
            var creator = new User { Id = "1", UserName = "CreatorUser" };
            var chatRoom = new ChatRoom("Test Room", creator);
            var user = new User { Id = "2", UserName = "NewUser" };

            // Act
            chatRoom.AddUser(user);

            // Assert
            Assert.Equal(2, chatRoom.Users.Count); 
            Assert.Contains(creator, chatRoom.Users);
            Assert.Contains(user, chatRoom.Users);
        }

        [Fact]
        public void AddUser_ShouldThrowException_WhenUserIsDuplicate()
        {
            // Arrange
            var creator = new User { Id = "1", UserName = "CreatorUser" };
            var chatRoom = new ChatRoom("Test Room", creator);


            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => chatRoom.AddUser(creator));
            Assert.Equal("User is already a member of the room.", exception.Message);
        }

        [Fact]
        public void AddUser_ShouldThrowException_WhenUserIsNull()
        {
            // Arrange
            var creator = new User { Id = "1", UserName = "CreatorUser" };
            var chatRoom = new ChatRoom("Test Room", creator);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chatRoom.AddUser(null));
            Assert.Contains("User cannot be null.", exception.Message);
        }

        [Fact]
        public void RemoveUser_ShouldRemoveUser_WhenUserExists()
        {
            // Arrange
            var creator = new User { Id = "1", UserName = "CreatorUser" };
            var user = new User { Id = "2", UserName = "User1" };
            var chatRoom = new ChatRoom("Test Room", creator);
            chatRoom.AddUser(user);

            // Act
            chatRoom.RemoveUser(user);

            // Assert
            Assert.Equal(1, chatRoom.Users.Count); // Only the creator should remain
            Assert.DoesNotContain(user, chatRoom.Users);
        }

        [Fact]
        public void RemoveUser_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var creator = new User { Id = "1", UserName = "CreatorUser" };
            var user = new User { Id = "2", UserName = "User1" };
            var chatRoom = new ChatRoom("Test Room", creator);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => chatRoom.RemoveUser(user));
            Assert.Equal("User is not a member of the room.", exception.Message);
        }

        [Fact]
        public void SendMessage_ShouldThrowException_WhenUserIsNull()
        {
            // Arrange
            var creator = new User { Id = "1", UserName = "CreatorUser" };
            var chatRoom = new ChatRoom("Test Room", creator);
            string messageContent = "Hello World!";

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chatRoom.SendMessage(null, messageContent));
            Assert.Equal("User cannot be null. (Parameter 'user')", exception.Message);
        }

        [Fact]
        public void SendMessage_ShouldThrowException_WhenContentIsNull()
        {
            // Arrange
            var creator = new User { Id = "1", UserName = "CreatorUser" };
            var chatRoom = new ChatRoom("Test Room", creator);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => chatRoom.SendMessage(creator, null));
            Assert.Equal("Content cannot be null or empty. (Parameter 'content')", exception.Message);
        }

        [Fact]
        public void SendMessage_ShouldThrowException_WhenContentIsEmpty()
        {
            // Arrange
            var creator = new User { Id = "1", UserName = "CreatorUser" };
            var chatRoom = new ChatRoom("Test Room", creator);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => chatRoom.SendMessage(creator, ""));
            Assert.Equal("Content cannot be null or empty. (Parameter 'content')", exception.Message);
        }

        [Fact]
        public void SendMessage_ShouldThrowException_WhenUserIsNotInRoom()
        {
            // Arrange
            var creator = new User { Id = "1", UserName = "CreatorUser" };
            var newUser = new User { Id = "2", UserName = "NewUser" };
            var chatRoom = new ChatRoom("Test Room", creator);
            string messageContent = "Hello World!";

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => chatRoom.SendMessage(newUser, messageContent));
            Assert.Equal("User must be a member of the room to send messages.", exception.Message);
        }

        [Fact]
        public void SendMessage_ShouldAddMessage_WhenUserIsValid()
        {
            // Arrange
            var creator = new User { Id = "1", UserName = "CreatorUser" };
            var chatRoom = new ChatRoom("Test Room", creator);
            string messageContent = "Hello World!";

            // Act
            chatRoom.SendMessage(creator, messageContent);

            // Assert
            Assert.Single(chatRoom.Messages);
            var message = chatRoom.Messages.First();
            Assert.Equal(creator.Id, message.UserId);
            Assert.Equal(messageContent, message.Content);
            Assert.Equal(chatRoom.Id, message.ChatId);
            Assert.NotEqual(default(DateTime), message.TimeSent);
        }
    }
}
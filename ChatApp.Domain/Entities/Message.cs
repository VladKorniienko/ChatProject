namespace ChatApp.Domain.Entities
{
    public class Message
    {
        public string Id { get; private set; }
        public string Content { get; private set; }
        public DateTime TimeSent { get; private set; }
        public string UserId {  get; private set; }
        public User User { get; private set; }
        public string ChatId { get; private set; }
        public ChatRoom Chat { get; private set; }

        private Message() { }
        public Message(string userId, string content, ChatRoom chat)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Message content cannot be empty.");

            Id = Guid.NewGuid().ToString();
            Content = content;
            TimeSent = DateTime.UtcNow;
            UserId = userId;
            ChatId = chat.Id;
            Chat = chat;
        }
    }
}

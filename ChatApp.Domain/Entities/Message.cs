namespace ChatApp.Domain.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
        public string UserId {  get; set; }
        public User User { get; set; }
        public string ChatId { get; set; }
        public ChatRoom Chat { get; set; }
    }
}

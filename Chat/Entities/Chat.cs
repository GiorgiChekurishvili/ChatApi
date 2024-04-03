namespace Chat.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }

        public int? MessageId { get; set; }
        public Message Message { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

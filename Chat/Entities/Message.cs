namespace Chat.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Chat> Chat { get; set; }
    }
}

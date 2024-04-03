namespace Chat.DTOs
{
    public class MessageForReceiveDto
    {
        public string SenderName { get; set; }
        public required string Text { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

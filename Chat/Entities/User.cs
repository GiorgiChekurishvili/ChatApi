using System.ComponentModel.DataAnnotations;

namespace Chat.Entities
{
    public class User
    {
        
        
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public  byte[] PasswordHash { get; set; }
        public  byte[] PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Chat> SendedMessages { get; set; }
        public ICollection<Chat> ReceivedMessages { get; set; }


    }
}

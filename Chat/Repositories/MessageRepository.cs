using Chat.DTOs;
using Chat.Entities;

namespace Chat.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatContext _context;

        public MessageRepository(ChatContext context)
        {
            _context = context;
        }
        public IEnumerable<MessageForReceiveDto> GetAllByUserId(int userId)
        {
            var messagesForReturn = _context.Chats
                .Where(chat => chat.ReceiverId == userId) 
                .Select(chat => new MessageForReceiveDto
                {
                    Text = chat.Message.Text, 
                    DateCreated = chat.Message.DateCreated, 
                    SenderName = _context.Users 
                        .Where(user => user.Id == chat.SenderId) 
                        .Select(user => user.UserName) 
                        .FirstOrDefault() 
                }).OrderBy(message => message.DateCreated).ToList();
            return messagesForReturn;
        }

        public Message Insert(Message message, int receiverId, int senderId)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
            _context.Chats.Add(new Entities.Chat{ ReceiverId = receiverId, SenderId = senderId, MessageId = message.Id });

            _context.SaveChanges();
            
            return message;
        }
    }
}

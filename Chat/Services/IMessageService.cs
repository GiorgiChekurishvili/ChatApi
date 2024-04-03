using Chat.DTOs;
using Chat.Entities;

namespace Chat.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageForReceiveDto>> GetAllMessages(int id);
        MessageForSendDto Send(MessageForSendDto message, int receiverId, int senderId);
    }
}

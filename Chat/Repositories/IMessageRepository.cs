using Chat.DTOs;
using Chat.Entities;

namespace Chat.Repositories
{
    public interface IMessageRepository
    {
        IEnumerable<MessageForReceiveDto> GetAllByUserId(int id);
        Message Insert (Message message, int receiverId, int senderId);

    }
}

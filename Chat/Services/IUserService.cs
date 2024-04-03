using Chat.DTOs;
using Chat.Entities;

namespace Chat.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserForReturnDto>> GetAllUsers();
        UserForReturnDto GetById(int id);
    }
}

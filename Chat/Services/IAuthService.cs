using Chat.DTOs;
using Chat.Entities;

namespace Chat.Services
{
    public interface IAuthService
    {
        Task<UserForRegisterDto> Register(UserForRegisterDto user, string password);
        Task<string> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}

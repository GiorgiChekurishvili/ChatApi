using Chat.Entities;

namespace Chat.Repositories
{
    public interface IAuthRepository
    {
        Task<User> LoginRepository(string username, string password);
        Task<User> RegisterRepository(User user, string password);
        Task<bool> UserExistsRepository(string username);
    }
}

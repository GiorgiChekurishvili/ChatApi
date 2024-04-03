using Chat.Entities;

namespace Chat.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById (int id);
    }
}

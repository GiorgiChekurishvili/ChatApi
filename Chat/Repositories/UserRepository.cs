using Chat.Entities;

namespace Chat.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatContext _context;
        public UserRepository(ChatContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

    }
}

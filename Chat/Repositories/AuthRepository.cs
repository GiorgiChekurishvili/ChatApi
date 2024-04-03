using Chat.Entities;
using Chat.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Chat.Entities;

namespace Chat.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ChatContext _context;
        public AuthRepository(ChatContext context)
        {

            _context = context;

        }
        public async Task<User> LoginRepository(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
            {
                return null;
            }
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;

        }

        public async Task<User> RegisterRepository(User user, string password)
        {
            byte[] passwordhash, passwordsalt;
            CreatePasswordHash(password, out passwordhash, out passwordsalt);
            user.PasswordHash = passwordhash;
            user.PasswordSalt = passwordsalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public Task<bool> UserExistsRepository(string username)
        {
            var user = _context.Users.AnyAsync(x => x.UserName == username);
            return user;
        }

        public bool VerifyPasswordHash(string password, byte[] passwordhash, byte[] passwordsalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordsalt))
            {
                var computehash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computehash.Length; i++)
                {
                    if (computehash[i] != passwordhash[i])
                    {
                        return false;
                    }

                }
                return true;
            }
        }

        public void CreatePasswordHash(string password, out byte[] passwordhash, out byte[] passwordsalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}

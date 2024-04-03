using AutoMapper;
using Chat.DTOs;
using Chat.Entities;
using Chat.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chat.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        
        public AuthService(IAuthRepository authRepository, IMapper mapper, IConfiguration config)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _config = config;
            

        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _authRepository.LoginRepository(username, password);
            if (user == null)
            {
                return null;
            }
            var claims = new[]
{
                new Claim(ClaimTypes.NameIdentifier,  user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = credentials
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokendescriptor);
            

            return tokenhandler.WriteToken(token);


        }

        public async Task<UserForRegisterDto> Register(UserForRegisterDto user, string password)
        {
            var usermap = _mapper.Map<User>(user);
            var repo = await _authRepository.RegisterRepository(usermap, password);
            var data = _mapper.Map<UserForRegisterDto>(repo);
            return data;
        }

        public async Task<bool> UserExists(string username)
        {
            var user = await _authRepository.UserExistsRepository(username);
            return user;
        }
    }
}

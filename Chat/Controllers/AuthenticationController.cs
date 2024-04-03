using Chat.DTOs;
using Chat.Entities;
using Chat.Repositories;
using Chat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authservice;
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly ChatContext _context;

        public AuthenticationController(IAuthService authService, IConfiguration configuration, IAuthRepository authRepository, ChatContext context)
        {
            _authservice = authService;
            _configuration = configuration;
            _authRepository = authRepository;
            _context = context;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();

            if (await _authservice.UserExists(userForRegisterDto.UserName))
            {
                return BadRequest("user already exists");
            }
            
            UserForRegisterDto user = new()
            {
                UserName = userForRegisterDto.UserName,
                Email = userForRegisterDto.Email,
                Password = userForRegisterDto.Password
                
            };

            var createduser = await _authservice.Register(user, userForRegisterDto.Password);

            return Ok();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {

            var token = await _authservice.Login(userForLoginDto.UserName.ToLower(), userForLoginDto.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            
            return Ok(new
            {
                token,
                userForLoginDto

            });
        }
        
    }
}


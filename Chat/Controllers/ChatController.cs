using AutoMapper;
using Chat.DTOs;
using Chat.Entities;
using Chat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMessageService _messageservice;
        private readonly IUserService _userservice;
        
        public ChatController(IMessageService messageService, IUserService userService)
        {
            _messageservice = messageService;
            _userservice = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserForReturnDto>>> GetAllUsers()
        {
            return Ok( await _userservice.GetAllUsers());
        }

        [Authorize]
        [HttpGet("GetAllMessages")]
        public async Task<IActionResult> GetAllMessages()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var allMessages = await _messageservice.GetAllMessages(userId);
            return Ok(allMessages);
        }

        [Authorize]
        [HttpPost("SendMessage")]
        public  IActionResult SendMessage([FromBody] MessageForSendDto messageForSend, [FromQuery]int receiverId)
        {
            var senderId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
           
            _messageservice.Send(messageForSend, receiverId, senderId);
            return Ok();


        }



        
    }
}

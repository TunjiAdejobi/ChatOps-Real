using ChatOps.Common;
using ChatOps.Service.Interfaces;
using ChatOps.Services.Interfaces;
using ChatOps.Services.Services;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ChatOps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)            
        {
            _userService = userService;            
        }

        [HttpPost]
        [Route("Push-UserActions")]
        public async Task<IActionResult> PushUser( [FromBody] Model.Models.Chats user)        
        {
            await _userService.PushUser(user);
            return Ok("Done");
        }

        [HttpGet]
        [Route("Push-Message")]
        public async Task<IActionResult> PushMessage(string message)
        {
            await _userService.PushMessage(message);
            return Ok("Done");
        }     
    }
}

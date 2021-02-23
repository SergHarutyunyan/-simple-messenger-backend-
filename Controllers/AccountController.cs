using System.Collections.Generic;
using System.Threading.Tasks;
using MessengerAPI.Managers;
using MessengerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MessengerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private UserManager _userManager;

        public AccountController(UserManager manager) {
            _userManager = manager;
        }

        [Authorize]
        [AllowAnonymous]      
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SignUpModel userParam)
        {
            var user = await _userManager.CreateUser(userParam.Email, userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "User already exists in database" });

            return Ok(new { AccountId = user.ID, Username = user.Username, AuthenticationData = user.AuthenticationData });
        }

        [Authorize]
        [AllowAnonymous] 
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel userParam)
        {
            var user = await _userManager.FindUser(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Wrong email or password" });

            return Ok(new { AccountId = user.ID, Username = user.Username, AuthenticationData = user.AuthenticationData });
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetUserList([FromQuery(Name = "user")]string currentUser)
        {
            var users = await _userManager.GetAllUsers(currentUser);

            if(users == null)
                return BadRequest(new { message = "No user exist" });
            
            return Ok(new { userList = users });
        }
    }
}
using MessengerAPI.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MessengerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
         private ChatManager _userManager;

        public ChatController(ChatManager manager) {
            _userManager = manager;
        }

    }
}
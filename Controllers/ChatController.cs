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
        private ChatManager _chatManager;

        public ChatController(ChatManager manager) {
            _chatManager = manager;
        }

        [Authorize]
        [HttpPost("newchannel")]
        public Task<IActionResult> CreateChatChannel(){
            
        }

    }
}
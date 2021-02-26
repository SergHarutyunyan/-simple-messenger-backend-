using System.Threading.Tasks;
using simple_messenger_backend.Managers;
using simple_messenger_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace simple_messenger_backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private ChatManager _chatManager;

        public ChatController(ChatManager manager) {
            _chatManager = manager;
        }

        [Authorize]
        [HttpPost("getchannel")]
        public async Task<IActionResult> GetChannel([FromBody] GetChatModel chatModel) 
        {
            var channel = await _chatManager.FindChannel(chatModel.User1, chatModel.User2);

            return Ok(new { chat = channel });
        }
    }
}
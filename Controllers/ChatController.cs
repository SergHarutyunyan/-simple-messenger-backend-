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
        [HttpGet("history")]
        public async Task<IActionResult> GetChannel(string user1, string user2) 
        {
            var chatHistory = await _chatManager.GetChatHistory(user1, user2);

            return Ok(new { chat = chatHistory });
        }
    }
}
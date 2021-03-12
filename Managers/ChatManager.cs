using System.Collections.Generic;
using System.Linq;
using simple_messenger_backend.Connectivity;
using Microsoft.EntityFrameworkCore;
using simple_messenger_backend.Models;
using System.Threading.Tasks;

namespace simple_messenger_backend.Managers
{
    public class ChatManager
    {
        private DataManager _dbContext;

          public ChatManager(DataManager context){
            _dbContext = context;
        }

        public async Task<IEnumerable<MessageResponse>> GetChatHistory(string user1, string user2) 
        {      
            var getMessages = await Task.Run(() => _dbContext.Messages
                .Include(u => u.From)
                .Include(u => u.To)
                .Where(m => (m.From.Username == user1 && m.To.Username == user2) || (m.To.Username == user1 && m.From.Username == user2)));

            List<MessageResponse> messages = new List<MessageResponse>();

            foreach(var message in getMessages){
                messages.Add(new MessageResponse {
                                 From = message.From.Username,
                                 To = message.To.Username,
                                 MessageText = message.MessageText,
                                 SendTime = message.SendTime });
            }

            return messages;
        }

    }
}
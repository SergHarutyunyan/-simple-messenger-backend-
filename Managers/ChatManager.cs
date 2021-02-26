using System.Collections.Generic;
using System.Linq;
using simple_messenger_backend.Connectivity;
using simple_messenger_backend.Models;
using System.Threading.Tasks;

namespace simple_messenger_backend.Managers
{
    public class ChatManager
    {
        private Context _dbContext;

          public ChatManager(Context context){
            _dbContext = context;
        }

        public async Task<ChatChannel> FindChannel(string user1, string user2) {     

            ChatChannel channel = await Task.Run(() =>
            {
                return _dbContext.Channels.Where(chat => (user1.Equals(chat.User1) && user2.Equals(chat.User2)) ||  (user2.Equals(chat.User1) && user1.Equals(chat.User2))).FirstOrDefault();           
            });

            if(channel != null)
                return channel;

            ChatChannel newChannel = new ChatChannel {
                User1 = user1,
                User2 = user2
            };

            _dbContext.Channels.Add(newChannel);
            _dbContext.SaveChanges();

            return newChannel;
        }

    }
}
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

        // public async Task<Channel> FindChannel(string user) 
        // {         
        //     var channel = await _dbContext.Channels.Include(c => c.User).SingleOrDefaultAsync(u => u.User.Username == user);
        //     return channel;
        // }

    }
}
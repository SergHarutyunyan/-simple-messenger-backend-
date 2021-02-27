using System.Threading.Tasks;
using simple_messenger_backend.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using simple_messenger_backend.Connectivity;
using System;

namespace simple_messenger_backend.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private DataManager _dbContext;
        private static List<Channel> UserChannels = new List<Channel>();

        public ChatHub(DataManager context) {
            _dbContext = context;
        }

        public void SendChatMessage(string who, string message)
        {
            // var name = Context.User.Identity.Name;
        
            // var user = _dbContext.Users.Find(who);
            // if (user == null)
            // {
            //     Clients.Caller.SendAsync("showErrorMessage", "Could not find that user.");
            // }
            // else
            // {
            //     _dbContext.Entry(user)
            //         .Collection(u => u.Connections)
            //         .Query()
            //         .Where(c => c.Connected == true)
            //         .Load();

            //     if (user.Connections == null)
            //     {
            //         Clients.Caller.SendAsync("showErrorMessage", "Could not find that user.");
            //     }
            //     else
            //     {
            //         foreach (var connection in user.Connections)
            //         {
            //             Clients.Client(connection.ConnectionID)
            //                 .addChatMessage(name + ": " + message);
            //         }
            //     }
            // }
            
        }

        public override async Task OnConnectedAsync()
        {
            string userName = Context.User.Identity.Name;
            
            User user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == userName);

            if(user != null) {              
                user.Connected = true;
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();     

                UserChannels.Add(new Channel { User = user, ConnectionID = Context.ConnectionId });
            }           
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {                           
            string userName = Context.User.Identity.Name;
            
            User user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == userName);

            if(user != null) { 
                user.Connected = false;
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();     

                var channel = UserChannels.SingleOrDefault(ch => ch.User.Username == userName);
                UserChannels.Remove(channel);
            }     
        }
    }
}

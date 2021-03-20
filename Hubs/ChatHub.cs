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

        public async Task<Message> SendChatMessage(string message, string from, string to)
        {        
            User fromUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username.Equals(from));
            User toUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username.Equals(to));

            if (toUser == null)
            {
                await Clients.Caller.SendAsync("showErrorMessage", "Could not find that user.");
                return null;
            }
            else
            {           
                
                Channel channel = UserChannels.Where(c => c.User.Email.Equals(toUser.Email)).FirstOrDefault();
                Message msg = await SaveMessage(fromUser, toUser, message);

                if(channel != null) {
                    await Clients.Client(channel.ConnectionID)
                        .SendAsync("ReceiveMessage", new {from = msg.From.Username, to = msg.To.Username, messageText = msg.MessageText, sendTime = msg.SendTime});
                }

                return msg;           
            }           
        }

        private async Task<Message> SaveMessage(User from, User to, string message) {
            
            Message newMessage = new Message {
                From = from,
                To = to,
                MessageText = message,
                SendTime = DateTime.Now
            };

            await _dbContext.Messages.AddAsync(newMessage);
            await _dbContext.SaveChangesAsync();

            return newMessage;
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

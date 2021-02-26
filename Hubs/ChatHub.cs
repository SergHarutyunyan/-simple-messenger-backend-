using System.Threading.Tasks;
using simple_messenger_backend.Models;
using Microsoft.AspNetCore.SignalR;
using simple_messenger_backend.Hubs.Clients;


namespace simple_messenger_backend.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.ReceiveMessage(message);
        }
    }
}
using System.Threading.Tasks;
using SignalR.MessengerAPI.Models;
using Microsoft.AspNetCore.SignalR;
using SignalR.MessengerAPI.Hubs.Clients;


namespace SignalR.MessengerAPI.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(ChatMessage message)
        {
            await Clients.All.ReceiveMessage(message);
        }
    }
}
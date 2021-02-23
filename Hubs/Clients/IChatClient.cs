using System.Threading.Tasks;
using SignalR.MessengerAPI.Models;

namespace SignalR.MessengerAPI.Hubs.Clients
{
    public interface IChatClient
    {
        Task ReceiveMessage(ChatMessage message);
    }
}
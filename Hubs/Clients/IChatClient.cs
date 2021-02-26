using System.Threading.Tasks;
using simple_messenger_backend.Models;

namespace simple_messenger_backend.Hubs.Clients
{
    public interface IChatClient
    {
        Task ReceiveMessage(Message message);
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol.Plugins;
namespace Gym.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string senderId, string receiverId, string senderName, string message, string attachmentPath)
        {
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, senderName, message, attachmentPath);
        }
    }
}

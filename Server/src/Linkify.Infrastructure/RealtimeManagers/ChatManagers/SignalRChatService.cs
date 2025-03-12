using Linkify.Application.ExternalServices;
using Microsoft.AspNetCore.SignalR;

namespace Linkify.Infrastructure.RealtimeManagers.ChatManagers
{
    public class SignalRChatService : IChatService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public SignalRChatService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageAsync(Guid receiverId, string message)
        {
            await _hubContext.Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", message);
        }
    }
}

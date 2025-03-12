using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Infrastructure.RealtimeManagers.ChatManagers
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Guid receiverId, string message)
        {
            await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", message);
        }
    }
}

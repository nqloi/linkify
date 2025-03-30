using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Infrastructure.RealtimeManagers.NotificationManagers
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(Guid userId, string notification)
        {
            await Clients.User(userId.ToString()).SendAsync("ReceiveNotification", notification);
        }
    }
}

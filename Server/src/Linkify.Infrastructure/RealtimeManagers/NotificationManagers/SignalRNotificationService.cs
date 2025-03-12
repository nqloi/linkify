using Linkify.Application.ExternalServices;
using Linkify.Domain.Aggregates.NotificationAggregate;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Infrastructure.RealtimeManagers.NotificationManagers
{
    public class SignalRNotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SignalRNotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(Guid userId, string notification)
        {
            await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", notification);
        }

        public async Task SendNotificationAsync(Notification notification)
        {
            await _hubContext.Clients.User(notification.UserId.ToString()).SendAsync("ReceiveNotification", notification);
        }

        public async Task SendToMultipleUsersAsync(IEnumerable<Guid> userIds, string notification)
        {
            foreach (var userId in userIds)
            {
                await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", notification);
            }
        }

        public async Task SendToGroupAsync(string groupName, string notification)
        {
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveNotification", notification);
        }
    }
}

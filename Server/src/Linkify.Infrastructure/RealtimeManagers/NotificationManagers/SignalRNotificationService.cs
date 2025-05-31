using Linkify.Application.ExternalServices;
using Linkify.Domain.Aggregates.NotificationAggregate;
using Linkify.Domain.Enums.Notification;
using Microsoft.AspNetCore.SignalR;

namespace Linkify.Infrastructure.RealtimeManagers.NotificationManagers
{
    public class SignalRNotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SignalRNotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(Notification notification)
        {
            var tasks = notification.Recipients
                .Select(recipient =>
                    _hubContext.Clients
                        .User(recipient.RecipientId.ToString())
                        .SendAsync("ReceiveNotification", notification)
                );
            await Task.WhenAll(tasks);
        }

        public async Task SendSystemNotificationAsync(string title, string message, string? actionUrl = null)
        {
            var notification = new Notification(
                Guid.Empty, // System notifications don't have a specific sender
                title,
                message,
                NotificationType.SystemNotification,
                actionUrl
            );

            // Send to all connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }

        public async Task SendSystemNotificationToGroupAsync(string groupName, string title, string message, string? actionUrl = null)
        {
            var notification = new Notification(
                Guid.Empty,
                title,
                message,
                NotificationType.SystemNotification,
                actionUrl
            );

            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveNotification", notification);
        }

        public async Task MarkNotificationAsReadAsync(Guid userId, Guid notificationId)
        {
            // Send real-time update to the specific user
            await _hubContext.Clients.User(userId.ToString()).SendAsync("NotificationRead", notificationId);
        }

        public async Task MarkAllNotificationsAsReadAsync(Guid userId)
        {
            // Send real-time update to the specific user
            await _hubContext.Clients.User(userId.ToString()).SendAsync("AllNotificationsRead");
        }

        public async Task SendToGroupAsync(string groupName, string notification)
        {
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveNotification", notification);
        }

        public async Task SendToGroupAsync(string groupName, Notification notification)
        {
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveNotification", notification);
        }

    }
}

using Microsoft.AspNetCore.SignalR;
using Linkify.Application.Common.DTOs.Notifications;

namespace Linkify.Infrastructure.RealtimeManagers.SignalR
{
    /// <summary>
    /// Interface defining the client-side methods that can be called from the server
    /// This will be used to generate TypeScript definitions
    /// </summary>
    public interface INotificationClient
    {
        Task ReceiveNotification(NotificationDto notification);
    }

    /// <summary>
    /// Strongly-typed hub with client methods interface
    /// </summary>
    public class NotificationHubWithTypedClient : Hub<INotificationClient>
    {
    }
}

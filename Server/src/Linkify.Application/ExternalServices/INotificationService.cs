using Linkify.Domain.Aggregates.NotificationAggregate;

namespace Linkify.Application.ExternalServices
{
    public interface INotificationService
    {
        // Core notification methods
        Task SendNotificationAsync(Notification notification);
        Task SendToGroupAsync(string groupName, string notification);
        Task SendToGroupAsync(string groupName, Notification notification);
        
        // System notification methods
        Task SendSystemNotificationAsync(string title, string message, string? actionUrl = null);
        Task SendSystemNotificationToGroupAsync(string groupName, string title, string message, string? actionUrl = null);
        
        // Notification status methods
        Task MarkNotificationAsReadAsync(Guid userId, Guid notificationId);
        Task MarkAllNotificationsAsReadAsync(Guid userId);
    }
}

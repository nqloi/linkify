using Linkify.Domain.Enums.Notification;

namespace Linkify.Application.Common.DTOs.Notifications
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public NotificationType Type { get; set; }
        public string? ActionUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Sender info
        public Guid SenderId { get; set; }
        public string? SenderDisplayName { get; set; }
        public string? SenderAvatarUrl { get; set; }
        public string? SenderUsername { get; set; }
        
        // Recipient-specific info
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}

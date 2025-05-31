using Linkify.Application.Features.Common;
using Linkify.Domain.Enums.Notification;

namespace Linkify.Application.Features.Notifications.Common
{
    public class GetNotificationDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ActionUrl { get; set; }

        public UserSummaryDto? Sender { get; set; } = null;
    }
}

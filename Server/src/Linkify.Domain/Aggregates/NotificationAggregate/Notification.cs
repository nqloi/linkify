using Linkify.Domain.Bases;
using Linkify.Domain.Enums.Notification;
using Linkify.Domain.Interfaces;

namespace Linkify.Domain.Aggregates.NotificationAggregate
{
    public class Notification : BaseEntity, IAggregateRoot
    {
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
        public NotificationType Type { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string? ActionUrl { get; private set; }

        private Notification() { }

        public Notification(Guid userId, string title, string message, NotificationType type, string? actionUrl = null) : base()
        {
            UserId = userId;
            Title = title;
            Message = message;
            Type = type;
            IsRead = false;
            ActionUrl = actionUrl;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}
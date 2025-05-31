using Linkify.Domain.Aggregates.UserProfileAggregate;
using Linkify.Domain.Bases;
using Linkify.Domain.Enums.Notification;
using Linkify.Domain.Interfaces;

namespace Linkify.Domain.Aggregates.NotificationAggregate
{
    public class Notification : BaseEntity, IAggregateRoot
    {
        public Guid SenderId { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
        public NotificationType Type { get; private set; }
        public string? ActionUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Navigation properties
        public UserProfile Sender { get; private set; }
        public ICollection<NotificationRecipient> Recipients { get; private set; }

        private Notification() 
        {
            Recipients = new List<NotificationRecipient>();
        }

        public Notification(
            Guid senderId, 
            string title, 
            string message, 
            NotificationType type, 
            string? actionUrl = null) : this()
        {
            SenderId = senderId;
            Title = title;
            Message = message;
            Type = type;
            ActionUrl = actionUrl;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddRecipient(Guid recipientId)
        {
            var recipient = new NotificationRecipient(Id, recipientId);
            Recipients.Add(recipient);
        }

        public void AddRecipients(IEnumerable<Guid> recipientIds)
        {
            foreach (var recipientId in recipientIds)
            {
                AddRecipient(recipientId);
            }
        }
    }
}

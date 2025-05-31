using Linkify.Domain.Aggregates.UserProfileAggregate;
using Linkify.Domain.Bases;

namespace Linkify.Domain.Aggregates.NotificationAggregate
{
    public class NotificationRecipient : BaseEntityAudit
    {
        public Guid NotificationId { get; private set; }
        public Guid RecipientId { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }

        // Navigation properties  
        public Notification Notification { get; private set; }
        public UserProfile Recipient { get; private set; }

        private NotificationRecipient() { }

        public NotificationRecipient(Guid notificationId, Guid recipientId)
        {
            NotificationId = notificationId;
            RecipientId = recipientId;
            IsRead = false;
        }

        public void MarkAsRead()
        {
            if (!IsRead)
            {
                IsRead = true;
                ReadAt = DateTime.UtcNow;
            }
        }
    }
}

using Linkify.Domain.Aggregates.NotificationAggregate;

namespace Linkify.Domain.Specifications.Notifications
{
    public class NotificationByUserIdSpecification : BaseSpecification<Notification>
    {
        public NotificationByUserIdSpecification(Guid userId)
        {
            Criteria = p => p.UserId == userId;
        }
    }
}

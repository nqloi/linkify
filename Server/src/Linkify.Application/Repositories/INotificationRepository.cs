using Linkify.Application.Common.DTOs.Notifications;
using Linkify.Application.Common.Models;
using Linkify.Application.Features.Notifications.Queries.GetPaginated;
using Linkify.Application.Features.Posts.Queries.GetByUserId;
using Linkify.Domain.Aggregates.NotificationAggregate;
using Linkify.Domain.Specifications.Notifications;

namespace Linkify.Application.Repositories
{
    public interface INotificationRepository : IBaseCommandRepository<Notification>
    {
        Task<NotificationRecipient?> GetRecipientEntry(Guid notificationId, Guid userId);
        
        Task<IEnumerable<NotificationRecipient>> GetAllUnreadByUserId(Guid userId);
        
        Task MarkAllAsRead(Guid userId);
        
        Task DeleteAllForUser(Guid userId);

        Task<int> GetUnreadCountForUser(Guid userId);

        Task<CursorPaginatedResult<NotificationDto>> GetPaginatedNotificationsForUser(NotificationByUserIdSpecification spec, GetPagedNotificationsQuery pagingParams, CancellationToken cancellationToken = default);
    }
}

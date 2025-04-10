using Linkify.Application.Common.Models;
using Linkify.Application.Features.Notifications.Common;
using Linkify.Application.Features.Notifications.Queries.GetPaginated;
using Linkify.Domain.Aggregates.NotificationAggregate;
using Linkify.Domain.Specifications.Notifications;

namespace Linkify.Application.Repositories
{
    public interface INotificationRepository : IBaseCommandRepository<Notification>
    {
        Task<CursorPaginatedResult<GetNotificationDto>> GetPagedNotificationsAsync(
            NotificationByUserIdSpecification spec,
            GetPagedNotificationsQuery pagingParams,
            CancellationToken cancellationToken);
            
        Task<Notification?> GetByIdAndUserIdAsync(Guid notificationId, Guid userId, CancellationToken cancellationToken);
        Task MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken);
        Task DeleteAllAsync(Guid userId, CancellationToken cancellationToken);
        Task<int> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken);
    }
}

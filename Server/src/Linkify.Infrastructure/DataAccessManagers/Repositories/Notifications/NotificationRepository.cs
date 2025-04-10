using AutoMapper;
using Linkify.Application.Common.Models;
using Linkify.Application.Extensions;
using Linkify.Application.Features.Notifications.Common;
using Linkify.Application.Features.Notifications.Queries.GetPaginated;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.NotificationAggregate;
using Linkify.Domain.Shared;
using Linkify.Domain.Specifications.Notifications;
using Linkify.Infrastructure.DataAccessManagers.Context;
using Linkify.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Linkify.Infrastructure.DataAccessManagers.Repositories.Notifications
{
    public class NotificationRepository : BaseCommandRepository<Notification>, INotificationRepository
    {
        private readonly IMapper _mapper;

        public NotificationRepository(ApplicationDbContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public async Task<CursorPaginatedResult<GetNotificationDto>> GetPagedNotificationsAsync(
            NotificationByUserIdSpecification spec,
            GetPagedNotificationsQuery pagingParams,
            CancellationToken cancellationToken)
        {
            var query = SpecificationEvaluator
                .GetQuery(_dbSet.AsNoTracking().AsQueryable(), spec)
                .OrderByDescending(n => n.CreatedAt)
                .ThenBy(n => n.Id)
                .Select(notification => new GetNotificationDto
                {
                    Id = notification.Id,
                    Title = notification.Title,
                    Message = notification.Message,
                    Type = notification.Type,
                    IsRead = notification.IsRead,
                    CreatedAt = notification.CreatedAt,
                    ActionUrl = notification.ActionUrl
                })
                .ApplyIsDeletedFilter();

            var sortCriteriaList = new[]
            {
                new SortCriteria(nameof(GetNotificationDto.CreatedAt), true),
                new SortCriteria(nameof(GetNotificationDto.Id))
            };

            return await query.ApplyCursorPagination(pagingParams, sortCriteriaList);
        }

        public async Task<Notification?> GetByIdAndUserIdAsync(
            Guid notificationId,
            Guid userId,
            CancellationToken cancellationToken)
        {
            return await _dbSet
                .FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId, cancellationToken);
        }

        public async Task MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken)
        {
            var notifications = await _dbSet
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync(cancellationToken);

            foreach (var notification in notifications)
            {
                notification.MarkAsRead();
            }
        }

        public async Task DeleteAllAsync(Guid userId, CancellationToken cancellationToken)
        {
            var notifications = await _dbSet
                .Where(n => n.UserId == userId)
                .ToListAsync(cancellationToken);

            _dbSet.RemoveRange(notifications);
        }

        public async Task<int> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _dbSet
                .CountAsync(n => n.UserId == userId && !n.IsRead, 
                    cancellationToken);
        }
    }
}

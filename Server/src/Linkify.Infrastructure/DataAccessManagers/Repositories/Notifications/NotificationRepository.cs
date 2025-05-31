using Linkify.Application.Common.DTOs.Notifications;
using Linkify.Application.Common.Models;
using Linkify.Application.Extensions;
using Linkify.Application.Features.Common;
using Linkify.Application.Features.Notifications.Queries.GetPaginated;
using Linkify.Application.Features.Posts.Common;
using Linkify.Application.Features.Posts.Queries.GetByUserId;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.NotificationAggregate;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Shared;
using Linkify.Domain.Specifications.Notifications;
using Linkify.Domain.Specifications.Posts;
using Linkify.Infrastructure.DataAccessManagers.Context;
using Linkify.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace Linkify.Infrastructure.DataAccessManagers.Repositories.Notifications
{
    public class NotificationRepository : BaseCommandRepository<Notification>, INotificationRepository
    {
        public DbSet<NotificationRecipient> Recipients => _context.Set<NotificationRecipient>();

        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<NotificationRecipient?> GetRecipientEntry(Guid notificationId, Guid userId)
        {
            return await Recipients
                .Include(nr => nr.Notification)
                    .ThenInclude(n => n.Sender)
                .FirstOrDefaultAsync(nr =>
                    nr.NotificationId == notificationId &&
                    nr.RecipientId == userId);
        }

        public async Task<IEnumerable<NotificationRecipient>> GetAllUnreadByUserId(Guid userId)
        {
            return await Recipients
                .Include(nr => nr.Notification)
                    .ThenInclude(n => n.Sender)
                .Where(nr => nr.RecipientId == userId && !nr.IsRead)
                .OrderByDescending(nr => nr.Notification.CreatedAt)
                .ToListAsync();
        }

        public async Task MarkAllAsRead(Guid userId)
        {
            var unreadRecipients = await Recipients
                .Where(nr => nr.RecipientId == userId && !nr.IsRead)
                .ToListAsync();

            foreach (var recipient in unreadRecipients)
            {
                recipient.MarkAsRead();
            }
        }

        public async Task DeleteAllForUser(Guid userId)
        {
            var userRecipients = await Recipients
                .Where(nr => nr.RecipientId == userId)
                .ToListAsync();

            Recipients.RemoveRange(userRecipients);
        }

        public async Task<CursorPaginatedResult<NotificationDto>> GetPaginatedNotificationsForUser(
            NotificationByUserIdSpecification spec,
            GetPagedNotificationsQuery pagingParams, CancellationToken
            cancellationToken = default)
        {
            var sortCriteriaList = new[]
            {
                new SortCriteria(nameof(NotificationDto.CreatedAt), true),
                new SortCriteria(nameof(NotificationDto.Id))
            };

            return await SpecificationEvaluator
                .GetQuery(_dbSet.AsNoTracking().AsQueryable(), spec)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    CreatedAt = n.CreatedAt,
                    Title = n.Title,
                    Message = n.Message,
                    Type = n.Type,
                    SenderId = n.SenderId,
                    ActionUrl = n.ActionUrl,
                    SenderDisplayName = n.Recipients.First().Recipient.DisplayName,
                    SenderAvatarUrl = n.Recipients.First().Recipient.AvatarUrl,
                    SenderUsername = n.Recipients.First().Recipient.UserName,
                    IsRead = n.Recipients.First().IsRead
                })
                .ApplyIsDeletedFilter()
                .ApplyCursorPagination(pagingParams, sortCriteriaList, cancellationToken);
        }

        public async Task<int> GetUnreadCountForUser(Guid userId)
        {
            return await Recipients
                .CountAsync(nr => nr.RecipientId == userId && !nr.IsRead);
        }
    }
}

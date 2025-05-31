using AutoMapper;
using ErrorOr;
using Linkify.Application.Common.DTOs.Notifications;
using Linkify.Application.Common.Models;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.NotificationAggregate;
using Linkify.Domain.Specifications.Notifications;
using MediatR;

namespace Linkify.Application.Features.Notifications.Queries.GetPaginated
{
    public class GetPagedNotificationsHandler
        : BaseQueryHandler<Notification>,
          IRequestHandler<GetPagedNotificationsQuery, ErrorOr<CursorPaginatedResult<NotificationDto>>>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetPagedNotificationsHandler(IBaseQueryRepository<Notification> repository, ICurrentUserService currentUserService, IMapper mapper, INotificationRepository notificationRepository) : base(repository, currentUserService, mapper)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<ErrorOr<CursorPaginatedResult<NotificationDto>>> Handle(
            GetPagedNotificationsQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();

            var spec = new NotificationByUserIdSpecification(userId);

            return await _notificationRepository.GetPaginatedNotificationsForUser(spec, request);
        }
    }
}

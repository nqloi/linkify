using AutoMapper;
using ErrorOr;
using Linkify.Application.Common.Models;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Notifications.Common;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.NotificationAggregate;
using Linkify.Domain.Specifications.Notifications;
using MediatR;

namespace Linkify.Application.Features.Notifications.Queries.GetPaginated
{
    public class GetPagedNotificationsHandler : BaseQueryHandler<Notification>, 
        IRequestHandler<GetPagedNotificationsQuery, ErrorOr<CursorPaginatedResult<GetNotificationDto>>>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetPagedNotificationsHandler(
            IBaseQueryRepository<Notification> repository, 
            ICurrentUserService currentUserService,
            IMapper mapper,
            INotificationRepository notificationRepository) 
            : base(repository, currentUserService, mapper)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<ErrorOr<CursorPaginatedResult<GetNotificationDto>>> Handle(
            GetPagedNotificationsQuery request,
            CancellationToken cancellationToken)
        {
            // Ensure user can only access their own notifications
            if (request.UserId != _currentUserService.GetUserId())
            {
                return Error.Forbidden();
            }

            var spec = new NotificationByUserIdSpecification(request.UserId);
            var result = await _notificationRepository.GetPagedNotificationsAsync(spec, request, cancellationToken);

            return result;
        }
    }
}

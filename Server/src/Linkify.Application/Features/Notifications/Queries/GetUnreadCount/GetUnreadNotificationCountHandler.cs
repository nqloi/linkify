using AutoMapper;
using ErrorOr;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.NotificationAggregate;
using MediatR;

namespace Linkify.Application.Features.Notifications.Queries.GetUnreadCount
{
    public class GetUnreadNotificationCountHandler
        : BaseQueryHandler<Notification>,
          IRequestHandler<GetUnreadNotificationCountQuery, ErrorOr<int>>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetUnreadNotificationCountHandler(IBaseQueryRepository<Notification> repository, 
            ICurrentUserService currentUserService, IMapper mapper, 
            INotificationRepository notificationRepository) : base(repository, currentUserService, mapper)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<ErrorOr<int>> Handle(
            GetUnreadNotificationCountQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            var count = await _notificationRepository.GetUnreadCountForUser(userId);
            
            return count;
        }
    }
}

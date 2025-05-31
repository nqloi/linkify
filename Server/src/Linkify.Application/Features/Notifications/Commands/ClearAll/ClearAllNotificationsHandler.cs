using ErrorOr;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.NotificationAggregate;
using MediatR;

namespace Linkify.Application.Features.Notifications.Commands.ClearAll
{
    public class ClearAllNotificationsHandler
        : BaseCommandHandler<Notification, INotificationRepository>,
          IRequestHandler<ClearAllNotificationsCommand, ErrorOr<Unit>>
    {
        public ClearAllNotificationsHandler(INotificationRepository repository, 
            IUnitOfWork unitOfWork, 
            ICurrentUserService currentUserService) : base(repository, unitOfWork, currentUserService)
        {
        }

        public async Task<ErrorOr<Unit>> Handle(
            ClearAllNotificationsCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            
            await _repository.DeleteAllForUser(userId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

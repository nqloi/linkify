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
        public ClearAllNotificationsHandler(
            INotificationRepository repository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
            : base(repository, unitOfWork, currentUserService)
        {
        }

        public async Task<ErrorOr<Unit>> Handle(
            ClearAllNotificationsCommand request,
            CancellationToken cancellationToken)
        {
            // Ensure user can only clear their own notifications
            if (request.UserId != GetCurrentUserId())
            {
                return Error.Forbidden();
            }

            await _repository.DeleteAllAsync(request.UserId, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

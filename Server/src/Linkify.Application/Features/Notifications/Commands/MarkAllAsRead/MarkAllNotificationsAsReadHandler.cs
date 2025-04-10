using ErrorOr;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.NotificationAggregate;
using MediatR;

namespace Linkify.Application.Features.Notifications.Commands.MarkAllAsRead
{
    public class MarkAllNotificationsAsReadHandler 
        : BaseCommandHandler<Notification, INotificationRepository>,
        IRequestHandler<MarkAllNotificationsAsReadCommand, ErrorOr<Unit>>
    {
        public MarkAllNotificationsAsReadHandler(
            INotificationRepository repository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
            : base(repository, unitOfWork, currentUserService)
        {
        }

        public async Task<ErrorOr<Unit>> Handle(
            MarkAllNotificationsAsReadCommand request,
            CancellationToken cancellationToken)
        {
            // Ensure user can only mark their own notifications as read
            if (request.UserId != GetCurrentUserId())
            {
                return Error.Forbidden();
            }

            await _repository.MarkAllAsReadAsync(request.UserId, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

using ErrorOr;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.NotificationAggregate;
using MediatR;

namespace Linkify.Application.Features.Notifications.Commands.MarkAsRead
{
    public class MarkNotificationAsReadHandler 
        : BaseCommandHandler<Notification, INotificationRepository>,
        IRequestHandler<MarkNotificationAsReadCommand, ErrorOr<Unit>>
    {
        public MarkNotificationAsReadHandler(
            INotificationRepository repository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
            : base(repository, unitOfWork, currentUserService)
        {
        }

        public async Task<ErrorOr<Unit>> Handle(
            MarkNotificationAsReadCommand request,
            CancellationToken cancellationToken)
        {
            // Ensure user can only mark their own notifications as read
            if (request.UserId != GetCurrentUserId())
            {
                return Error.Forbidden();
            }

            var notification = await _repository.GetByIdAndUserIdAsync(
                request.NotificationId,
                request.UserId,
                cancellationToken);

            if (notification == null)
            {
                return Error.NotFound("Notification not found");
            }

            notification.MarkAsRead();
            await _unitOfWork.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

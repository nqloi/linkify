using ErrorOr;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.NotificationAggregate;
using MediatR;

namespace Linkify.Application.Features.Notifications.Commands.Delete
{
    public class DeleteNotificationHandler 
        : BaseCommandHandler<Notification, INotificationRepository>,
        IRequestHandler<DeleteNotificationCommand, ErrorOr<Unit>>
    {
        public DeleteNotificationHandler(
            INotificationRepository repository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
            : base(repository, unitOfWork, currentUserService)
        {
        }

        public async Task<ErrorOr<Unit>> Handle(
            DeleteNotificationCommand request,
            CancellationToken cancellationToken)
        {
            // Ensure user can only delete their own notifications
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

            _repository.Delete(notification);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

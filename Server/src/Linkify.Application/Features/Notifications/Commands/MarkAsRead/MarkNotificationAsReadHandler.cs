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
        private readonly INotificationService _notificationService;

        public MarkNotificationAsReadHandler(
            INotificationRepository repository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork,
            INotificationService notificationService) : base(repository, unitOfWork, currentUserService)
        {
            _notificationService = notificationService;
        }

        public async Task<ErrorOr<Unit>> Handle(
            MarkNotificationAsReadCommand request,
            CancellationToken cancellationToken)
        {
            try 
            {
                var userId = _currentUserService.GetUserId();
                var recipient = await _repository.GetRecipientEntry(request.NotificationId, userId);

                if (recipient is null)
                {
                    return Error.NotFound("Notification.NotFound", "Notification not found");
                }

                recipient.MarkAsRead();
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Send real-time update to client
                await _notificationService.MarkNotificationAsReadAsync(userId, request.NotificationId);

                return Unit.Value;
            }
            catch (Exception)
            {
                return Error.Failure(
                    "Notification.MarkAsReadFailed",
                    "Failed to mark notification as read");
            }
        }
    }
}

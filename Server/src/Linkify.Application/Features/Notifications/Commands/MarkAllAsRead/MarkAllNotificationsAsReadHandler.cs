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
        private readonly INotificationService _notificationService;

        public MarkAllNotificationsAsReadHandler(
            INotificationRepository repository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork,
            INotificationService notificationService) : base(repository, unitOfWork, currentUserService)
        {
            _notificationService = notificationService;
        }

        public async Task<ErrorOr<Unit>> Handle(
            MarkAllNotificationsAsReadCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var userId = _currentUserService.GetUserId();
                
                await _repository.MarkAllAsRead(userId);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Send real-time update to client
                await _notificationService.MarkAllNotificationsAsReadAsync(userId);

                return Unit.Value;
            }
            catch (Exception)
            {
                return Error.Failure(
                    "Notifications.MarkAllAsReadFailed",
                    "Failed to mark all notifications as read");
            }
        }
    }
}

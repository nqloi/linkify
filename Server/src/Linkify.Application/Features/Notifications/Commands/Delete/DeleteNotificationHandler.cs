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
        public DeleteNotificationHandler(INotificationRepository repository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : base(repository, unitOfWork, currentUserService)
        {
        }

        public async Task<ErrorOr<Unit>> Handle(
            DeleteNotificationCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            
            var recipient = await _repository.GetRecipientEntry(request.NotificationId, userId);

            if (recipient is null)
            {
                return Error.NotFound("Notification.NotFound", "Notification not found");
            }

            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

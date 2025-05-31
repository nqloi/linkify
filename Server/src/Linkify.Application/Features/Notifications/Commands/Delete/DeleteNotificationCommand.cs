using ErrorOr;
using MediatR;

namespace Linkify.Application.Features.Notifications.Commands.Delete
{
    public class DeleteNotificationCommand : IRequest<ErrorOr<Unit>>
    {
        public required Guid NotificationId { get; init; }
    }
}

using ErrorOr;
using MediatR;

namespace Linkify.Application.Features.Notifications.Commands.MarkAsRead
{
    public class MarkNotificationAsReadCommand : IRequest<ErrorOr<Unit>>
    {
        public required Guid NotificationId { get; init; }
    }
}

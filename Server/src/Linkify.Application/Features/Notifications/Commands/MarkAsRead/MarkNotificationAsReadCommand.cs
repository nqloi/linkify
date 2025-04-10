using ErrorOr;
using MediatR;

namespace Linkify.Application.Features.Notifications.Commands.MarkAsRead
{
    public class MarkNotificationAsReadCommand : IRequest<ErrorOr<Unit>>
    {
        public Guid NotificationId { get; set; }
        public Guid UserId { get; set; }
    }
}

using ErrorOr;
using MediatR;

namespace Linkify.Application.Features.Notifications.Commands.Delete
{
    public class DeleteNotificationCommand : IRequest<ErrorOr<Unit>>
    {
        public Guid NotificationId { get; set; }
        public Guid UserId { get; set; }
    }
}

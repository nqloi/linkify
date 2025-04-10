using ErrorOr;
using MediatR;

namespace Linkify.Application.Features.Notifications.Commands.MarkAllAsRead
{
    public class MarkAllNotificationsAsReadCommand : IRequest<ErrorOr<Unit>>
    {
        public Guid UserId { get; set; }
    }
}

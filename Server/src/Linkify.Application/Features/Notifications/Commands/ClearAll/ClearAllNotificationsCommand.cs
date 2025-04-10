using ErrorOr;
using MediatR;

namespace Linkify.Application.Features.Notifications.Commands.ClearAll
{
    public class ClearAllNotificationsCommand : IRequest<ErrorOr<Unit>>
    {
        public Guid UserId { get; set; }
    }
}

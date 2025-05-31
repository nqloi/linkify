using ErrorOr;
using MediatR;

namespace Linkify.Application.Features.Notifications.Commands.ClearAll
{
    public class ClearAllNotificationsCommand : IRequest<ErrorOr<Unit>>
    {
        // Empty command since we only need the current user ID from the handler
    }
}

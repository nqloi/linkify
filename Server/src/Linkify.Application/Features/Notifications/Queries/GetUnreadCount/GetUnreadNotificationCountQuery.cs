using ErrorOr;
using MediatR;

namespace Linkify.Application.Features.Notifications.Queries.GetUnreadCount
{
    public class GetUnreadNotificationCountQuery : IRequest<ErrorOr<int>>
    {
        // Empty query class since we only need the current user ID from the handler
    }
}

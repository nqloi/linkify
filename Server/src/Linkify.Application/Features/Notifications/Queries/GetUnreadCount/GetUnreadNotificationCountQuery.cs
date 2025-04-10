using ErrorOr;
using MediatR;

namespace Linkify.Application.Features.Notifications.Queries.GetUnreadCount
{
    public class GetUnreadNotificationCountQuery : IRequest<ErrorOr<int>>
    {
    }
}

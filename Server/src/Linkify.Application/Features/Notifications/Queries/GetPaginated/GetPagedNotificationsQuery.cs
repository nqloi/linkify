using ErrorOr;
using Linkify.Application.Common.Models;
using Linkify.Application.Features.Notifications.Common;
using MediatR;

namespace Linkify.Application.Features.Notifications.Queries.GetPaginated
{
    public class GetPagedNotificationsQuery : CursorPaginationParameters, IRequest<ErrorOr<CursorPaginatedResult<GetNotificationDto>>>
    {
        public Guid UserId { get; set; }
    }
}

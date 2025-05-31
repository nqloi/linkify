using ErrorOr;
using Linkify.Application.Common.DTOs.Notifications;
using Linkify.Application.Common.Models;
using MediatR;

namespace Linkify.Application.Features.Notifications.Queries.GetPaginated
{
    public class GetPagedNotificationsQuery : CursorPaginationParameters, IRequest<ErrorOr<CursorPaginatedResult<NotificationDto>>>
    {

    }
}

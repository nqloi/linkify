using ErrorOr;
using Linkify.Application.Common.Models;
using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Notifications.Commands.ClearAll;
using Linkify.Application.Features.Notifications.Commands.Delete;
using Linkify.Application.Features.Notifications.Commands.MarkAllAsRead;
using Linkify.Application.Features.Notifications.Commands.MarkAsRead;
using Linkify.Application.Features.Notifications.Common;
using Linkify.Application.Features.Notifications.Queries.GetPaginated;
using Linkify.Application.Features.Notifications.Queries.GetUnreadCount;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace Linkify.Api.Controllers;

public class NotificationsController : BaseApiController
{
    private readonly ICurrentUserService _currentUserService;

    public NotificationsController(ISender sender, ICurrentUserService currentUserService) 
        : base(sender)
    {
        _currentUserService = currentUserService;
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginatedNotifications([FromQuery] CursorPaginationParameters pagingParams)
    {
        var query = new GetPagedNotificationsQuery
        {
            UserId = _currentUserService.GetUserId(),
            Cursor = pagingParams.Cursor,
            Limit = pagingParams.Limit
        };

        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [HttpPut("{notificationId}/read")]
    public async Task<IActionResult> MarkAsRead(Guid notificationId)
    {
        var command = new MarkNotificationAsReadCommand
        {
            NotificationId = notificationId,
            UserId = _currentUserService.GetUserId()
        };

        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [HttpPut("mark-all-read")]
    public async Task<IActionResult> MarkAllAsRead()
    {
        var command = new MarkAllNotificationsAsReadCommand
        {
            UserId = _currentUserService.GetUserId()
        };

        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("{notificationId}")]
    public async Task<IActionResult> Delete(Guid notificationId)
    {
        var command = new DeleteNotificationCommand
        {
            NotificationId = notificationId,
            UserId = _currentUserService.GetUserId()
        };

        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("clear-all")]
    public async Task<IActionResult> ClearAll()
    {
        var command = new ClearAllNotificationsCommand
        {
            UserId = _currentUserService.GetUserId()
        };

        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadCount()
    {
        var query = new GetUnreadNotificationCountQuery();
        var result = await _sender.Send(query);
        return HandleResult(result);
    }
}

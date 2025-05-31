using Linkify.Application.Common.DTOs.Notifications;
using Linkify.Application.Common.Models;
using Linkify.Application.Features.Notifications.Commands.ClearAll;
using Linkify.Application.Features.Notifications.Commands.CreateNotification;
using Linkify.Application.Features.Notifications.Commands.Delete;
using Linkify.Application.Features.Notifications.Commands.MarkAllAsRead;
using Linkify.Application.Features.Notifications.Commands.MarkAsRead;
using Linkify.Application.Features.Notifications.Queries.GetPaginated;
using Linkify.Application.Features.Notifications.Queries.GetUnreadCount;
using Linkify.Domain.Enums.Notification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace Linkify.Api.Controllers
{
    /// <summary>
    /// Controller for managing notifications
    /// </summary>
    public class NotificationsController : BaseApiController
    {
        public NotificationsController(ISender sender) : base(sender)
        {
        }

        /// <summary>
        /// Gets paginated notifications for the current user
        /// </summary>
        /// <returns>Paginated list of notifications</returns>
        [HttpGet]
        public async Task<IActionResult> GetNotifications(
            [FromQuery] GetPagedNotificationsQuery pagingParams)
        {
            var result = await _sender.Send(pagingParams);

            return HandleResult(result);
        }

        /// <summary>
        /// Gets the count of unread notifications for the current user
        /// </summary>
        /// <returns>Number of unread notifications</returns>
        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            var query = new GetUnreadNotificationCountQuery();
            var result = await _sender.Send(query);

            return HandleResult(result);
        }

        /// <summary>
        /// Creates a new notification
        /// </summary>
        /// <param name="command">Notification creation command</param>
        /// <returns>Result of the operation</returns>
        [HttpPost]
        public async Task<IActionResult> CreateNotification(CreateNotificationCommand command)
        {
            var result = await _sender.Send(command);

            return HandleResult(result);
        }

        /// <summary>
        /// Creates a system notification
        /// </summary>
        /// <param name="message">The notification message</param>
        /// <param name="recipientIds">List of recipient user IDs</param>
        /// <param name="actionUrl">Optional URL for notification action</param>
        /// <returns>Result of the operation</returns>
        [HttpPost("system")]
        [Authorize(Roles = "Admin")] // Only admins can send system notifications
        public async Task<IActionResult> CreateSystemNotification(
            [FromBody] CreateSystemNotificationRequest request)
        {
            var command = new CreateNotificationCommand
            {
                Type = NotificationType.SystemNotification,
                Message = request.Message,
                RecipientIds = request.RecipientIds,
                ActionUrl = request.ActionUrl
            };

            var result = await _sender.Send(command);
            return HandleResult(result);
        }

        /// <summary>
        /// Marks a notification as read
        /// </summary>
        /// <param name="id">Notification ID</param>
        /// <returns>Result of the operation</returns>
        [HttpPatch("{id}/mark-as-read")]
        public async Task<IActionResult> MarkAsRead(Guid id)
        {
            var command = new MarkNotificationAsReadCommand { NotificationId = id };
            var result = await _sender.Send(command);

            return HandleResult(result);
        }

        /// <summary>
        /// Marks all notifications as read for the current user
        /// </summary>
        /// <returns>Result of the operation</returns>
        [HttpPatch("mark-all-as-read")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var command = new MarkAllNotificationsAsReadCommand();
            var result = await _sender.Send(command);

            return HandleResult(result);
        }

        /// <summary>
        /// Deletes a specific notification
        /// </summary>
        /// <param name="id">Notification ID</param>
        /// <returns>Result of the operation</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNotificationCommand { NotificationId = id };
            var result = await _sender.Send(command);

            return HandleResult(result);
        }

        /// <summary>
        /// Clears all notifications for the current user
        /// </summary>
        /// <returns>Result of the operation</returns>
        [HttpDelete]
        public async Task<IActionResult> ClearAll()
        {
            var command = new ClearAllNotificationsCommand();
            var result = await _sender.Send(command);

            return HandleResult(result);
        }
    }
}

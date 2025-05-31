using ErrorOr;
using Linkify.Domain.Enums.Notification;
using MediatR;

namespace Linkify.Application.Features.Notifications.Commands.CreateNotification
{
    public class CreateNotificationCommand : IRequest<ErrorOr<Unit>>
    {
        public required NotificationType Type { get; init; }
        public required IEnumerable<Guid> RecipientIds { get; init; }
        public string? Message { get; init; }
        public string? ActionUrl { get; init; }
        public string? AdditionalInfo { get; init; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Linkify.Application.Common.DTOs.Notifications
{
    public class CreateSystemNotificationRequest
    {
        [Required(ErrorMessage = "Message is required")]
        [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters")]
        public required string Message { get; init; }

        [Required(ErrorMessage = "At least one recipient is required")]
        [MinLength(1, ErrorMessage = "At least one recipient is required")]
        public required IEnumerable<Guid> RecipientIds { get; init; }

        [StringLength(2000, ErrorMessage = "Action URL cannot exceed 2000 characters")]
        public string? ActionUrl { get; init; }
    }
}

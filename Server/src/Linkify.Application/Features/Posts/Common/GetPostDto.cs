using Linkify.Application.Features.Common;

namespace Linkify.Application.Features.Posts.Common
{
    public record GetPostDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = "";

        public List<string> ImageUrls { get; set; } = [];

        public DateTime CreatedAt { get; set; }
        public PostStatsDto Stats { get; set; } = new();
        public required UserSummaryDto Creator { get; set; }
        public required UserPostActionsDto UserActions { get; set; }
    }
}

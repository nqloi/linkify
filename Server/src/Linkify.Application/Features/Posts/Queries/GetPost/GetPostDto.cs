using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Posts.Queries.GetPost
{
    public record GetPostDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; } = "";

        public List<string> ImageUrls { get; set; } = [];

        public string? CreatorName { get; set; }
        public string? CreatorAvatarUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int ReactionCount { get; set; }
        public int CommentCount { get; set; }
    }
}

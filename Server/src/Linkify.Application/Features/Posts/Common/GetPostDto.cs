using Linkify.Application.Features.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Posts.Common
{
    public record GetPostDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = "";

        public List<string> ImageUrls { get; set; } = [];

        public DateTime? CreatedAt { get; set; }
        public PostStatsDto Stats { get; set; } = new();
        public required CreatorDto Creator { get; set; }
        public required UserPostActionsDto UserActions { get; set; }
    }
}

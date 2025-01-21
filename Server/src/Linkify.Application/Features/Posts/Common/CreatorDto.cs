using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Posts.Common
{
    public record CreatorDto
    {
        public Guid Id { get; set; }
        public required string DisplayName { get; set; }
        public string? AvatarUrl { get; set; }
    }
}

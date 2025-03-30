using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Common
{
    public record CreatorDto
    {
        public Guid UserId { get; set; }
        public required string UserName { get; set; }
        public required string DisplayName { get; set; }
        public string? AvatarUrl { get; set; }
    }
}

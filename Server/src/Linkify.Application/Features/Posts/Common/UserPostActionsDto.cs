using Linkify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Posts.Common
{
    public record UserPostActionsDto
    {
        public ReactionType ReactionType { get; set; } = ReactionType.None;
        public bool IsSaved { get; set; } = false;
    }
}

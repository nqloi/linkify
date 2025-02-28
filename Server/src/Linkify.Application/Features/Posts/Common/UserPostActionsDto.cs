using Linkify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Posts.Common
{
    public class UserPostActionsDto
    {
        public ReactionType ReactionType { get; set; }
        public bool IsSaved { get; set; }
    }
}

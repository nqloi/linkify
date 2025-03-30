using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Posts.Common
{
    public class PostStatsDto
    {
        public int ReactionCount { get; set; }
        public int CommentCount { get; set; }
        public int ShareCount { get; set; }
    }
}

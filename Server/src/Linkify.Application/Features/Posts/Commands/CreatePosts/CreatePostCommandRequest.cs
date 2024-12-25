using Linkify.Application.Features.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Posts.Commands.CreatePosts
{
    public record CreatePostCommandRequest : IRequest<bool>
    {
        public required string Content { get; set; }
    }
}

using Linkify.Application.Common.Models;
using MediatR;

namespace Linkify.Application.Features.Posts.Commands.CreatePosts
{
    public record CreatePostCommand : IRequest<bool>
    {
        public string? Content { get; set; }
        public List<FileData>? Images { get; set; }
    }
}

using Linkify.Application.Common.Models;
using Linkify.Application.Features.Posts.Queries.GetPost;
using MediatR;

namespace Linkify.Application.Features.Posts.Commands.CreatePost
{
    public record CreatePostCommand : IRequest<GetPostDto>
    {
        public string? Content { get; set; }
        public List<FileData>? Images { get; set; }
    }
}

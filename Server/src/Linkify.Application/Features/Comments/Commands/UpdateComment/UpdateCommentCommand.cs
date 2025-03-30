using ErrorOr;
using MediatR;

namespace Linkify.Application.Features.Comments.Commands.UpdateComment
{
    public record UpdateCommentCommand : IRequest<ErrorOr<bool>>
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public required string NewContent { get; set; }
    }
}

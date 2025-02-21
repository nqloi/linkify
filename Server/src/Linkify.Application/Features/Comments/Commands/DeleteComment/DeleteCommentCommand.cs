using ErrorOr;
using MediatR;

namespace Linkify.Application.Features.Comments.Commands.DeleteComment
{
    public record DeleteCommentCommand : IRequest<ErrorOr<bool>>
    {
        public Guid CommentId { get; set; }

        public Guid PostId { get; set; }
    }
}

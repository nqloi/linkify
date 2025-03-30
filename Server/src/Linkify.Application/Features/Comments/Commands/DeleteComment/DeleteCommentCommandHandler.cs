using ErrorOr;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : BaseCommandHandler<Post>, IRequestHandler<DeleteCommentCommand, ErrorOr<bool>>
    {
        public DeleteCommentCommandHandler(IBaseCommandRepository<Post> repository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : base(repository, unitOfWork, currentUserService)
        {
        }

        public async Task<ErrorOr<bool>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetWithIncludesAsync(request.PostId, cancellationToken, p => p.Comments);

            if (post == null)
            {
                return Error.Validation("Comment.Delete", "Post does not exist");
            }

            var result = post.DeleteComment(request.CommentId);

            if(!result.IsSuccess)
            {
                return Error.Validation("Comment.Delete", result?.Error ?? "");
            }

            await _unitOfWork.SaveAsync(cancellationToken);

            return true;
        }
    }
}

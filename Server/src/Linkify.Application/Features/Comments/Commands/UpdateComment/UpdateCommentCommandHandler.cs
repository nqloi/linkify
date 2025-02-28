using ErrorOr;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Linkify.Application.Features.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : BaseCommandHandler<Post>, IRequestHandler<UpdateCommentCommand, ErrorOr<bool>>
    {
        public UpdateCommentCommandHandler(IBaseCommandRepository<Post> repository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : base(repository, unitOfWork, currentUserService)
        {
        }

        public async Task<ErrorOr<bool>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetWithIncludesAsync(request.PostId, cancellationToken, p => p.Comments);

            if (post == null)
            {
                return Error.Validation("Comment.Delete", "Post does not exist");
            }

            post.UpdateComment(request.CommentId, request.NewContent);

            await _unitOfWork.SaveAsync(cancellationToken);
            return true;
        }
    }
}

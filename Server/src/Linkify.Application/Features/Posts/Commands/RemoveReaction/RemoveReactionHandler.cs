using ErrorOr;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Linkify.Application.Features.Posts.Commands.RemoveReaction
{
    public class RemoveReactionHandler : BaseCommandHandler<Post>, IRequestHandler<RemoveReactionCommand, ErrorOr<bool>>
    {
        public RemoveReactionHandler(IBaseCommandRepository<Post> repository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : base(repository, unitOfWork, currentUserService)
        {
        }

        public async Task<ErrorOr<bool>> Handle(RemoveReactionCommand request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetWithIncludesAsync(request.PostId, cancellationToken, p => p.Reactions);

            if (post == null) return false;

            post.RemoveReaction(_currentUserService.GetUserId());
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}

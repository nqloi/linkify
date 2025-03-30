using ErrorOr;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Linkify.Application.Features.Posts.Commands.AddOrUpdateReaction
{
    public class AddOrUpdateReactionHandler : BaseCommandHandler<Post>, IRequestHandler<AddOrUpdateReactionCommand, ErrorOr<bool>>
    {
        public AddOrUpdateReactionHandler(IBaseCommandRepository<Post> repository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : base(repository, unitOfWork, currentUserService)
        {
        }

        public async Task<ErrorOr<bool>> Handle(AddOrUpdateReactionCommand request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetWithIncludesAsync(request.PostId, cancellationToken, p => p.Reactions);

            if (post == null) return false;

            post.AddOrUpdateReaction(_currentUserService.GetUserId(), request.Type);

            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}

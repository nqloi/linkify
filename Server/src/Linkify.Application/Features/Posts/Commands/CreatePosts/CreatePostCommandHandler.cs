using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Linkify.Application.Features.Posts.Commands.CreatePosts
{
    public class CreatePostCommandHandler : BaseCommandHandler<Post>, IRequestHandler<CreatePostCommandRequest, bool>
    {
        public ICurrentUserService _currentUserService;

        public CreatePostCommandHandler(
            IBaseCommandRepository<Post> repository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService) : base(repository, unitOfWork)
        {
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
        {
            var post = new Post(_currentUserService.GetUserId(), request.Content);

            await _repository.CreateAsync(post, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return true;
        }
    }
}

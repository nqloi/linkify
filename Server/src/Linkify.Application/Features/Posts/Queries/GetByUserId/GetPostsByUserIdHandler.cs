using AutoMapper;
using ErrorOr;
using Linkify.Application.Common.Models;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Posts.Common;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Specifications.Posts;
using MediatR;

namespace Linkify.Application.Features.Posts.Queries.GetByUserId
{
    public class GetPostsByUserIdHandler : BaseQueryHandler<Post>, IRequestHandler<GetPagedPostsByUserIdQuery, ErrorOr<CursorPaginatedResult<GetPostDto>>>
    {
        private readonly IPostRepository _postRepository;
        public GetPostsByUserIdHandler(IBaseQueryRepository<Post> repository, ICurrentUserService currentUserService, IMapper mapper, IPostRepository postRepository)
            : base(repository, currentUserService, mapper)
        {
            _postRepository = postRepository;
        }

        public async Task<ErrorOr<CursorPaginatedResult<GetPostDto>>> Handle(GetPagedPostsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new PostByUserIdSpecification(request.UserId);

            var result = await _postRepository.GetPagedUserPostsAsync(spec, request, cancellationToken);

            return result;
        }
    }
}

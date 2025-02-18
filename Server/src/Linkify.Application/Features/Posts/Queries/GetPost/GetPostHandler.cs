using AutoMapper;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Specifications.Posts;
using MediatR;

namespace Linkify.Application.Features.Posts.Queries.GetPost
{
    public class GetPostHandler : BaseQueryHandler<Post>, IRequestHandler<GetPostRequest, IEnumerable<GetPostDto>>
    {
        public GetPostHandler(IBaseQueryRepository<Post> repository, ICurrentUserService currentUserService, IMapper mapper) : base(repository, currentUserService, mapper)
        {
        }

        public async Task<IEnumerable<GetPostDto>> Handle(GetPostRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            var spec = new PostWithDetailsSpecification(userId);

            var posts = await _repository.GetWithSpecificationAsync(spec);

            var postDto = _mapper.Map<List<GetPostDto>>(posts);

            return postDto;
        }
    }
}

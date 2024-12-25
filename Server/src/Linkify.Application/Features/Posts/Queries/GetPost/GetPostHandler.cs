using AutoMapper;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Linkify.Application.Features.Posts.Queries.GetPost
{
    public class GetPostHandler : BaseQueryHandler<Post>, IRequestHandler<GetPostRequest, IEnumerable<GetPostDto>>
    {
        private readonly IMapper _mapper;

        public GetPostHandler(IBaseQueryRepository<Post> repository, ICurrentUserService currentUserService, IMapper mapper) : base(repository, currentUserService)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetPostDto>> Handle(GetPostRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();

            var posts = await _repository.FindAsync(item => item.UserId == userId);

            return _mapper.Map<IEnumerable<GetPostDto>>(posts);
        }
    }
}

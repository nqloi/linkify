using AutoMapper;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Aggregates.UserProfile;
using Linkify.Domain.Specifications.Posts;
using MediatR;

namespace Linkify.Application.Features.Posts.Queries.GetPost
{
    public class GetPostHandler : BaseQueryHandler<Post>, IRequestHandler<GetPostRequest, IEnumerable<GetPostDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBaseQueryRepository<UserProfile> _userProfileRepo;

        public GetPostHandler(IBaseQueryRepository<Post> repository, 
            ICurrentUserService currentUserService, 
            IMapper mapper, 
            IBaseQueryRepository<UserProfile> userProfileRepo) : base(repository, currentUserService)
        {
            _mapper = mapper;
            _userProfileRepo = userProfileRepo;
        }

        public async Task<IEnumerable<GetPostDto>> Handle(GetPostRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            var spec = new PostWithDetailsSpecification(userId);

            var posts = await _repository.GetWithSpecificationAsync(spec);

            var userIds = posts.Select(x => x.UserId).Distinct();
            var userProfiles = await _userProfileRepo.FindAsync(up => userIds.Contains(up.UserId));

            var userDictionary = userProfiles.ToDictionary(user => user.UserId);

            var postDto = posts.Select(p =>
            {
                return new GetPostDto
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    ImageUrls = p.PostImages.Select(p1 => p1.ImageUrl).ToList(),
                    Content = p.Content,
                    CreatorName = userDictionary[userId]?.DisplayName ?? "",
                    CreatorAvatarUrl = userDictionary[userId]?.AvatarUrl ?? "",
                    CreatedAt = p.CreatedAt,
                };
            });

            return postDto;
        }
    }
}

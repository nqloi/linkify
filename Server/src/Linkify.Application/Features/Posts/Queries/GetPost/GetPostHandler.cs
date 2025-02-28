using AutoMapper;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Common;
using Linkify.Application.Features.Posts.Common;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Enums;
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

            var query = _repository.GetWithSpecification(spec)
               .Select(post => new GetPostDto
               {
                   Id = post.Id,
                   Content = post.Content,
                   CreatedAt = post.CreatedAt,
                   Creator = new CreatorDto
                   {
                       Id = post.UserProfile.UserId,
                       DisplayName = post.UserProfile.DisplayName,
                       AvatarUrl = post.UserProfile.AvatarUrl,
                   },
                   Stats = new PostStatsDto
                   {
                       ReactionCount = post.Reactions.Count(),
                       CommentCount = post.Comments.Count()
                   },
                   UserActions = new UserPostActionsDto
                   {
                       ReactionType = post.Reactions.Where(r => r.UserId == userId).Select(r => r.Type).FirstOrDefault() 
                   },
                   ImageUrls = post.PostImages.Select(pi => pi.ImageUrl).ToList()
               });

            var result = await _repository.QueryAsync(query, cancellationToken);

            return result;
        }
    }
}

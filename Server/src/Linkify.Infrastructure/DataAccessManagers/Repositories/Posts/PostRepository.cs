using AutoMapper;
using Linkify.Application.Common.Models;
using Linkify.Application.Extensions;
using Linkify.Application.Features.Common;
using Linkify.Application.Features.Posts.Common;
using Linkify.Application.Features.Posts.Queries.GetByUserId;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Shared;
using Linkify.Domain.Specifications.Posts;
using Linkify.Infrastructure.DataAccessManagers.Context;
using Linkify.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Linkify.Infrastructure.DataAccessManagers.Repositories.Posts
{
    public class PostRepository : BaseQueryRepository<Post>, IPostRepository
    {
        private readonly IMapper _mapper;
        public PostRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<CursorPaginatedResult<GetPostDto>> GetPagedUserPostsAsync(
            PostByUserIdSpecification spec,
            GetPagedPostsByUserIdQuery pagingParams, CancellationToken
            cancellationToken = default)
        {

            Func<IEnumerable<Reaction>, Guid, UserPostActionsDto> getUserPostActions =
            (reactions, userId) =>
            {
                if (reactions.IsNullAndEmpty())
                {
                    return new UserPostActionsDto();
                }
                var userReaction = reactions.FirstOrDefault(r => r.UserId == userId);

                if (userReaction == null)
                {
                    return new UserPostActionsDto();
                }
                return new UserPostActionsDto { ReactionType = userReaction.Type };
            };

            var sortCriteriaList = new[]
            {
                new SortCriteria(nameof(GetPostDto.CreatedAt), true),
                new SortCriteria(nameof(GetPostDto.Id))
            };

            return await SpecificationEvaluator
                .GetQuery(_dbSet.AsNoTracking().AsQueryable(), spec)
                .Select(post => new GetPostDto
                {
                    Id = post.Id,
                    CreatedAt = post.CreatedAt,
                    Content = post.Content,
                    Creator = _mapper.Map<CreatorDto>(post.UserProfile),
                    Stats = new PostStatsDto
                    {
                        ReactionCount = post.Reactions.Count(),
                        CommentCount = post.Comments.Count()
                    },
                    UserActions = getUserPostActions(post.Reactions, pagingParams.UserId),
                    ImageUrls = post.PostImages.Select(pi => pi.ImageUrl).ToList()
                })
                .ApplyIsDeletedFilter()
                .ApplyCursorPagination(pagingParams, sortCriteriaList);
        }
    }
}

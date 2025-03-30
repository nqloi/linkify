using Linkify.Application.Common.Models;
using Linkify.Application.Features.Posts.Common;
using Linkify.Application.Features.Posts.Queries.GetByUserId;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Specifications.Posts;

namespace Linkify.Application.Repositories
{
    public interface IPostRepository : IBaseQueryRepository<Post>
    {
        Task<CursorPaginatedResult<GetPostDto>> GetPagedUserPostsAsync(PostByUserIdSpecification spec,
            GetPagedPostsByUserIdQuery pagingParams, 
            CancellationToken cancellationToken = default);
    }
}

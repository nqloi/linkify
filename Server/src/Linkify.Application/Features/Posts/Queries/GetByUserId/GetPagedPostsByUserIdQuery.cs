using ErrorOr;
using Linkify.Application.Common.Models;
using Linkify.Application.Features.Posts.Common;
using MediatR;

namespace Linkify.Application.Features.Posts.Queries.GetByUserId
{
    public class GetPagedPostsByUserIdQuery : CursorPaginationParameters, IRequest<ErrorOr<CursorPaginatedResult<GetPostDto>>>
    {
        public Guid UserId { get; set; }
    }
}

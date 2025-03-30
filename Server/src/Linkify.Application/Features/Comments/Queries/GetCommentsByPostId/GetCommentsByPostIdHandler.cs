using AutoMapper;
using ErrorOr;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Comments.Common;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Specifications.Comments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Comments.Queries.GetCommentsByPostId
{
    public class GetCommentsByPostIdHandler : BaseQueryHandler<Comment>, IRequestHandler<GetCommentsByPostIdRequest, ErrorOr<List<CommentDto>>>
    {
        public GetCommentsByPostIdHandler(IBaseQueryRepository<Comment> repository, ICurrentUserService currentUserService, IMapper mapper) : base(repository, currentUserService, mapper)
        {
        }

        public async Task<ErrorOr<List<CommentDto>>> Handle(GetCommentsByPostIdRequest request, CancellationToken cancellationToken)
        {
            if(request.PostId == Guid.Empty)
            {
                return Error.Validation("Post.Id", "PostId can not empty.");
            }
            var spec = new CommentByPostIdSpecification(request.PostId);
            
            var comments = await _repository.GetWithSpecificationAsync(spec, cancellationToken);
            var results = _mapper.Map<IEnumerable<CommentDto>>(comments);

            return results.ToList();
        }
    }
}

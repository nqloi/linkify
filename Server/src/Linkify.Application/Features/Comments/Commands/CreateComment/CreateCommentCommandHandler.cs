using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Comments.Common;
using Linkify.Application.Features.Posts.Commands.CreatePost;
using Linkify.Application.Features.Posts.Queries.GetPost;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler : BaseCommandHandler<Post>, IRequestHandler<CreateCommentCommand, Guid>
    {
        public CreateCommentCommandHandler(
            IBaseCommandRepository<Post> repository, 
            IUnitOfWork unitOfWork, 
            ICurrentUserService currentUserService) : base(repository, unitOfWork, currentUserService)
        {
        }

        public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetWithIncludesAsync(request.PostId, cancellationToken, p => p.Comments);

            if (post == null)
            {
                throw new ArgumentException("PostId does not exist");
            }

            Comment comment = new(_currentUserService.GetUserId(), request.PostId, request.Content);
            
            post.AddComment(comment);

            await _unitOfWork.SaveAsync(cancellationToken);

            return comment.Id;
        }
    }
}

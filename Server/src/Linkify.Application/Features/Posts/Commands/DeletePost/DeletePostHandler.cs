using Linkify.Application.Configurations;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Posts.Commands.CreatePost;
using Linkify.Application.Features.Posts.Queries.GetPost;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Posts.Commands.DeletePost
{
    public class DeletePostHandler : BaseCommandHandler<Post>, IRequestHandler<DeletePostRequest, bool>
    {
        private readonly IFileService _fileService;
        CloudFolderPathSettings _folderPath;

        public DeletePostHandler(
            IBaseCommandRepository<Post> repository, 
            IUnitOfWork unitOfWork, 
            IFileService fileService,
            ICurrentUserService currentUserService,
            IOptions<CloudFolderPathSettings> options) : base(repository, unitOfWork, currentUserService)
        {
            _fileService = fileService;
            _folderPath = options.Value;
        }

        public async Task<bool> Handle(DeletePostRequest request, CancellationToken cancellationToken)
        {
            _repository.DeleteById(request.PostId);
            await _fileService.DeleteFolderAsync($"{_folderPath.Post}/{request.PostId}");

            await _unitOfWork.SaveAsync(cancellationToken);
            return true;
        }
    }
}

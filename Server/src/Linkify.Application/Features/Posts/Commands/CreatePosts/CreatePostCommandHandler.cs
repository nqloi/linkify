using Linkify.Application.CQS;
using Linkify.Application.Extensions;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Linkify.Application.Features.Posts.Commands.CreatePosts
{
    public class CreatePostCommandHandler : BaseCommandHandler<Post>, IRequestHandler<CreatePostCommand, bool>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IFileService _fileService;

        public CreatePostCommandHandler(
            IBaseCommandRepository<Post> repository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService,
            IFileService fileService) : base(repository, unitOfWork)
        {
            _currentUserService = currentUserService;
            _fileService = fileService;
        }

        public async Task<bool> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            var post = new Post(_currentUserService.GetUserId(), request.Content);
            var imageUrls = new List<string>();

            if(request?.Images?.IsNotNullAndAny() ?? false)
            {
                var uploadTasks = request.Images.Select(file =>
                {
                    Stream stream = new MemoryStream(file.Content);
                    return (FileStream: stream, file.FileName);
                });

                imageUrls = (await _fileService.UploadMultipleFilesAsync(uploadTasks, $"post/{post.Id}")).ToList();
            }

            if (imageUrls?.IsNotNullAndAny() ?? false) {
                // Upload images and add URLs to the post
                foreach (var imageUrl in imageUrls)
                {
                    PostImage image = new (post.Id, imageUrl);
                    post.AddImage(image); // Directly leverage domain logic
                }
            }

            await _repository.CreateAsync(post, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return true;
        }
    }
}

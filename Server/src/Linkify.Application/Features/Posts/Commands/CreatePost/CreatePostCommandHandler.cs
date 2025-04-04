﻿using AutoMapper;
using Linkify.Application.CQS;
using Linkify.Application.Extensions;
using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Posts.Common;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using MediatR;

namespace Linkify.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : BaseCommandHandler<Post>, IRequestHandler<CreatePostCommand, GetPostDto>
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(
            IBaseCommandRepository<Post> repository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService,
            IFileService fileService,
            IMapper mapper) : base(repository, unitOfWork, currentUserService)
        {
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<GetPostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            var post = new Post(_currentUserService.GetUserId(), request.Content);

            await _repository.CreateAsync(post, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            var imageUrls = new List<string>();

            if (request?.Images?.IsNotNullAndAny() ?? false)
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

            return _mapper.Map<GetPostDto>(post);
        }
    }
}

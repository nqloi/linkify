using Linkify.Api.Common.Models;
using Linkify.Api.DTOs.Posts;
using Linkify.Application.Common.Models;
using Linkify.Application.Features.Posts.Commands.AddOrUpdateReaction;
using Linkify.Application.Features.Posts.Commands.CreatePost;
using Linkify.Application.Features.Posts.Commands.DeletePost;
using Linkify.Application.Features.Posts.Commands.RemoveReaction;
using Linkify.Application.Features.Posts.Common;
using Linkify.Application.Features.Posts.Queries.GetPost;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace Linkify.Api.Controllers
{
    public class PostController(ISender _sender) : BaseApiController(_sender)
    {
        [HttpPost]
        public async Task<ActionResult<ApiSuccessResult<GetPostDto>>> Create([FromForm] CreatePostRequest request, CancellationToken cancellationToken)
        {
            // Convert IFormFile to FileData
            var imageList = request.Images?.Select(file =>
            {
                using var memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);
                return new FileData
                {
                    FileName = file.FileName,
                    Content = memoryStream.ToArray()
                };
            }).ToList();

            // Create command request
            var command = new CreatePostCommand
            {
                Content = request.Content,
                Images = imageList
            };

            var result = await _sender.Send(command, cancellationToken);

            return Ok(new ApiSuccessResult<GetPostDto>
            {
                Content = result
            });
        }

        [HttpGet]
        public async Task<ActionResult<ApiSuccessResult<IEnumerable<GetPostDto>>>> Get(GetPostRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request, cancellationToken);

            return Ok(new ApiSuccessResult<IEnumerable<GetPostDto>>
            {
                Content = result
            });
        }

        [HttpDelete("{postId}")]
        public async Task<ActionResult<ApiSuccessResult<IEnumerable<GetPostDto>>>> DeleteById([FromRoute]Guid postId, CancellationToken cancellationToken)
        {
            var request = new DeletePostRequest { PostId = postId };
            var result = await _sender.Send(request, cancellationToken);

            return Ok(new ApiSuccessResult<bool>
            {
                Content = result
            });
        }

        [HttpPost("{postId}/reaction")]
        public async Task<IActionResult> AddOrUpdateReaction(Guid postId, [FromBody] AddOrUpdateReactionCommand request)
        {
            request.PostId = postId;
            var result = await _sender.Send(request);
            return HandleResult(result);
        }

        [HttpDelete("{postId}/reaction")]
        public async Task<IActionResult> RemoveReaction(Guid postId)
        {
            var command = new RemoveReactionCommand { PostId = postId };
            var result = await _sender.Send(command);

            return HandleResult(result);
        }
    }
}

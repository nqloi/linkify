using Linkify.Api.Common.Models;
using Linkify.Application.Features.Posts.Commands.CreatePosts;
using Linkify.Application.Features.Posts.Queries.GetPost;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace Linkify.Api.Controllers
{
    public class PostController(ISender _sender) : BaseApiController(_sender)
    {
        [HttpPost]
        public async Task<ActionResult<ApiSuccessResult<bool>>> Create([FromBody] CreatePostCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request, cancellationToken);

            return Ok(new ApiSuccessResult<bool>
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
    }
}

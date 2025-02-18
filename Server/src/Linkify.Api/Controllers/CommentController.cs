using Linkify.Api.Common.Models;
using Linkify.Application.Features.Comments.Commands.CreateComment;
using Linkify.Application.Features.Comments.Common;
using Linkify.Application.Features.Comments.Queries.GetCommentsByPostId;
using Linkify.Application.Features.Posts.Queries.GetPost;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace Linkify.Api.Controllers
{
    [Route("api/v{version:apiVersion}/post/{postId:guid}/comment")]
    public class CommentController(ISender _sender) : BaseApiController(_sender)
    {
        [HttpGet]
        public async Task<IActionResult> GetCommentsByPostId(Guid postId)
        {
            var query = new GetCommentsByPostIdRequest { PostId = postId };
            var result = await _sender.Send(query);

            return Ok(new ApiSuccessResult<IEnumerable<CommentDto>>
            {
                Content = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Guid postId, [FromBody] CreateCommentCommand command)
        {
            command.PostId = postId;
            var result = await _sender.Send(command);

            return Ok(new ApiSuccessResult<Guid>
            {
                Content = result
            });
        }

        //[HttpPut("{commentId}")]
        //public async Task<IActionResult> UpdateComment(string postId, string commentId, [FromBody] UpdateCommentRequest request)
        //{
        //    command.CommentId = commentId;
        //    await _mediator.Send(command);
        //    return NoContent();
        //}

        //[HttpDelete("{commentId}")]
        //public async Task<IActionResult> DeleteComment(string postId, string commentId)
        //{
        //    var command = new DeleteCommentCommand { CommentId = commentId };
        //    await _mediator.Send(command);
        //    return NoContent();
        //}

    }
}

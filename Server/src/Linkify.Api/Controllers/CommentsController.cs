using Linkify.Application.Features.Comments.Commands.CreateComment;
using Linkify.Application.Features.Comments.Commands.DeleteComment;
using Linkify.Application.Features.Comments.Commands.UpdateComment;
using Linkify.Application.Features.Comments.Queries.GetCommentsByPostId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace Linkify.Api.Controllers
{
    [Route("api/v{version:apiVersion}/posts/{postId:guid}/comments")]
    public class CommentsController(ISender _sender) : BaseApiController(_sender)
    {
        [HttpGet]
        public async Task<IActionResult> GetCommentsByPostId(Guid postId)
        {
            var query = new GetCommentsByPostIdRequest { PostId = postId };
            var result = await _sender.Send(query);

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Guid postId, [FromBody] CreateCommentCommand command)
        {
            command.PostId = postId;
            var result = await _sender.Send(command);

            return HandleResult(result);
        }

        [HttpPut("{commentId:guid}")]
        public async Task<IActionResult> UpdateComment(Guid commentId, Guid postId, [FromBody] UpdateCommentCommand request)
        {
            request.CommentId = commentId;
            request.PostId = postId;
            var result = await _sender.Send(request);
            return HandleResult(result);
        }

        [HttpDelete("{commentId:guid}")]
        public async Task<IActionResult> DeleteComment(Guid postId, Guid commentId)
        {
            var command = new DeleteCommentCommand { CommentId = commentId, PostId = postId };
            var result = await _sender.Send(command);
            return HandleResult(result);
        }

    }
}

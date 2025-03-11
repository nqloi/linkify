using Linkify.Api.Common.Models;
using Linkify.Application.Features.Friends.Commands.SendFriendRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using WebAPI.Controllers;

namespace Linkify.Api.Controllers
{
    public class FriendShipsController(ISender _sender) : BaseApiController(_sender)
    {
        [HttpPost("requests/{receiverId:guid}")]
        public async Task<IActionResult> SendFriendRequest([FromRoute] Guid receiverId, CancellationToken cancellationToken)
        {
            var command = new SendFriendRequestCommand(receiverId);
            var result = await _sender.Send(command, cancellationToken);
            return HandleResult(result);
        }

        //[HttpPost("requests/{requestId}/accept")]
        //public async Task<IActionResult> AcceptFriendRequest(Guid requestId)
        //{
           
        //}

        //[HttpPost("requests/{requestId}/reject")]
        //public async Task<IActionResult> RejectFriendRequest(Guid requestId)
        //{
            
        //}

        //[HttpGet("requests/pending")]
        //public async Task<IActionResult> GetPendingRequests()
        //{
           
        //}
    }
}

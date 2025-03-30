using Linkify.Api.Common.Models;
using Linkify.Application.Features.Friends.Commands.SendFriendRequest;
using Linkify.Application.Features.UserProfiles.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using WebAPI.Controllers;

namespace Linkify.Api.Controllers
{
    public class UserProfilesController(ISender _sender) : BaseApiController(_sender)
    {
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetUserProfileQuery(userId);
            var result = await _sender.Send(query, cancellationToken);
            return HandleResult(result);
        }
    }
}

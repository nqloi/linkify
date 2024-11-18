using Asp.Versioning;
using Linkify.Api.DTOs.Authentication;
using Linkify.Application.Features.Authentication.Commands.Login;
using Linkify.Application.Features.Authentication.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using WebAPI.Controllers;


namespace Linkify.Api.Controllers
{
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthenticationController(ISender sender, IConfiguration configuration) : BaseApiController(sender)
    {
        private readonly IConfiguration _configuration = configuration;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginCommandRequest( request?.UserName, request?.Password );
            var response = await _sender.Send(command, cancellationToken);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _sender.Send(request, cancellationToken);

            return Ok(response);
        }        
        
        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout(LoginRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginCommandRequest(request?.UserName, request?.Password);
            var response = await _sender.Send(command, cancellationToken);

            return Ok(response);
        }

    }
}

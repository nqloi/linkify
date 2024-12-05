using Asp.Versioning;
using Linkify.Api.Common.Models;
using Linkify.Api.DTOs.Authentication;
using Linkify.Application.Features.Authentication.Commands.Login;
using Linkify.Application.Features.Authentication.Commands.Register;
using Linkify.Application.Features.Authentication.Commands.Token;
using Linkify.Application.Features.Authentication.Common;
using Linkify.Domain.Constants;
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
        public async Task<ActionResult<ApiSuccessResult<AuthenticationResult>>> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginCommandRequest(request.UserName, request.Password);
            var response = await _sender.Send(command, cancellationToken);

            bool useHttpOnlyCookieForToken = false;
            var accessToken = response.AccessToken;
            var refreshToken = response.RefreshToken;

            if (_configuration["Jwt:UseHttpOnlyCookieForToken"] != null)
            {
                bool.TryParse(_configuration["Jwt:UseHttpOnlyCookieForToken"], out useHttpOnlyCookieForToken);
            }

            if (useHttpOnlyCookieForToken)
            {

                var accessTokenCookieName = _configuration["Jwt:accessTokenCookieName"];
                var refreshTokenCookieName = _configuration["Jwt:refreshTokenCookieName"];

                if (!double.TryParse(_configuration["Jwt:ExpireInMinute"], out double expireInMinute))
                {
                    expireInMinute = 15.0;
                }

                if (accessTokenCookieName != null)
                {
                    HttpContext.Response.Cookies.Delete(accessTokenCookieName);
                    HttpContext.Response.Cookies.Append(accessTokenCookieName, accessToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddMinutes(expireInMinute)
                    });
                }

                if (refreshTokenCookieName != null)
                {
                    HttpContext.Response.Cookies.Delete(refreshTokenCookieName);
                    HttpContext.Response.Cookies.Append(refreshTokenCookieName, refreshToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddDays(TokenConst.ExpiryInDays)
                    });

                }
            }

            return Ok(new ApiSuccessResult<AuthenticationResult>
            {
                Code = StatusCodes.Status200OK,
                Message = $"Success",
                Content = response
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<ApiSuccessResult<AuthenticationResult>>> RegisterAsync(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _sender.Send(request, cancellationToken);

            return Ok(new ApiSuccessResult<AuthenticationResult>
            {
                Code = StatusCodes.Status200OK,
                Message = $"Success",
                Content = response
            });
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout(LoginRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginCommandRequest(request.UserName, request.Password);
            var response = await _sender.Send(command, cancellationToken);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiSuccessResult<AuthenticationResult>>> RefreshToken([FromBody] string refreshtoken, CancellationToken cancellationToken)
        {
            var refreshTokenRequest = new RefreshTokenRequest { RefreshToken = refreshtoken };
            var response = await _sender.Send(refreshTokenRequest, cancellationToken);

            return Ok(new ApiSuccessResult<AuthenticationResult>
            {
                Code = StatusCodes.Status200OK,
                Message = $"Success",
                Content = response
            });
        }
    }
}

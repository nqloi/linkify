using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Authentication.Commands.Register;
using Linkify.Application.Features.Authentication.Common;
using Linkify.Domain.Aggregates.UserAggregate;
using Linkify.Infrastructure.SecurityManagers.Tokens;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Infrastructure.SecurityManagers.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public IdentityService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthenticationResult> LoginAsync(string? username, string? password, CancellationToken cancellationToken = default)
        {
            var user = _userManager.Users.FirstOrDefault(user => user.UserName == username);

            if (user == null) 
            {
                throw new UnauthorizedAccessException("Invalid login credentials.");
            }
            var accessToken = _tokenService.GenerateToken(user, []);
            var refreshToken = _tokenService.GenerateRefreshToken();
            return new AuthenticationResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
        
        public async Task<AuthenticationResult> RegisterAsync(RegisterCommandRequest registerCommandRequest, CancellationToken cancellationToken = default)
        {
            var applicationUser = new ApplicationUser(
               registerCommandRequest.UserName,
               registerCommandRequest.FirstName,
               registerCommandRequest.LastName
            );
            var result = await _userManager.CreateAsync(applicationUser, registerCommandRequest.Password);

            if (!result.Succeeded)
            {
                throw new IdentityException(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            var accessToken = _tokenService.GenerateToken(applicationUser, []);
            var refreshToken = _tokenService.GenerateRefreshToken();
            return new AuthenticationResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
    }
}

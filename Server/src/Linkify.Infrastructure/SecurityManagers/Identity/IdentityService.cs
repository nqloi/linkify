using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Authentication.Commands.Register;
using Linkify.Application.Features.Authentication.Common;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.Token;
using Linkify.Domain.Aggregates.UserAggregate;
using Linkify.Infrastructure.DataAccessManagers.Repositories;
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
        private readonly ITokenRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            ITokenRepository tokenRepository,
            IUnitOfWork unitOfWork,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }

        public async Task<AuthenticationResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            var user = _userManager.Users.FirstOrDefault(user => user.UserName == username);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid login credentials.");
            }
            var accessToken = _tokenService.GenerateToken(user, []);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var result = await _signInManager.PasswordSignInAsync(user, password, true, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid login credentials. NotSucceeded.");
            }

            var tokens = await _tokenRepository.GetByUserIdAsync(user.Id, cancellationToken);
            foreach (var tokenItem in tokens)
            {
                _tokenRepository.Purge(tokenItem);
            }

            var token = new Token(user.Id, refreshToken);
            await _tokenRepository.CreateAsync(token, cancellationToken);
            await _userManager.GenerateUserTokenAsync(user, "Default", "access_token");
            await _unitOfWork.SaveAsync(cancellationToken);
            return new AuthenticationResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
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

        public async Task<AuthenticationResult> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            var token = await _tokenRepository.GetByRefreshTokenAsync(refreshToken);

            if (token.ExpiryDate < DateTime.UtcNow)
            {
                throw new IdentityException("Refresh token has expired, please re-login");
            }

            var user = await _userManager.FindByIdAsync(token.UserId);

            if (user == null)
            {
                throw new IdentityException("Refresh token has expired, please re-login");
            }

            var newAccessToken = _tokenService.GenerateToken(user, new List<Claim>());
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            return new AuthenticationResult
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };
        }

    }
}

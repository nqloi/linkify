using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Authentication.Commands.Register;
using Linkify.Application.Features.Authentication.Common;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.Token;
using Linkify.Domain.Aggregates.UserProfileAggregate;
using Linkify.Infrastructure.SecurityManagers.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Linkify.Infrastructure.SecurityManagers.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IBaseCommandRepository<UserProfile> _userProfileRepo;

        public IdentityService(UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            ITokenRepository tokenRepository,
            IUnitOfWork unitOfWork,
            SignInManager<ApplicationUser> signInManager,
            IBaseCommandRepository<UserProfile> userProfileRepo)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userProfileRepo = userProfileRepo;
        }

        public async Task<AuthenticationResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByNameAsync(username);

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

        public async Task<bool> RegisterAsync(RegisterCommandRequest registerCommandRequest, CancellationToken cancellationToken = default)
        {
            var applicationUser = new ApplicationUser(
               registerCommandRequest.UserName,
               registerCommandRequest.FirstName,
               registerCommandRequest.LastName
            );

            var result = await _userManager.CreateAsync(applicationUser, registerCommandRequest.Password);

            var userProfile = new UserProfile() { 
                DisplayName = registerCommandRequest.FirstName + " " + registerCommandRequest.LastName, 
                UserId = applicationUser.Id
            };

            await _userProfileRepo.CreateAsync(userProfile);

            await _unitOfWork.SaveAsync(cancellationToken);

            if (!result.Succeeded)
            {
                throw new IdentityException(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
             
            return true;
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            var token = await _tokenRepository.GetByRefreshTokenAsync(refreshToken);

            if (token.ExpiryDate < DateTime.UtcNow)
            {
                throw new IdentityException("Refresh token has expired, please re-login");
            }

            var user = await _userManager.FindByIdAsync(token.UserId.ToString());

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

        public async Task<bool> CheckDuplicateUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null;
        }

    }
}

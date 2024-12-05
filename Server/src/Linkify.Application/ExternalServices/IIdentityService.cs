using Linkify.Application.Features.Authentication.Commands.Register;
using Linkify.Application.Features.Authentication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.ExternalServices
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default);
        Task<AuthenticationResult> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
        Task<AuthenticationResult> RegisterAsync(RegisterCommandRequest registerCommandRequest, CancellationToken cancellationToken = default);
    }
}

using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Authentication.Commands.Token
{
    public class RefreshTokenHandler(IIdentityService _identityService) : IRequestHandler<RefreshTokenRequest, AuthenticationResult>
    {
        public async Task<AuthenticationResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            return await _identityService.RefreshTokenAsync(request.RefreshToken, cancellationToken);
        }
    }
}

using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Authentication.Commands.Login
{
    public class LoginCommandHandler(IIdentityService _identityService) : IRequestHandler<LoginCommandRequest, AuthenticationResult>
    {

        public async Task<AuthenticationResult> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var authenticationResult = await _identityService.LoginAsync(request.UserName, request.Password, cancellationToken);

            return authenticationResult;
        }
    }
}

﻿using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Authentication.Commands.Login;
using Linkify.Application.Features.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Authentication.Commands.Register
{
    public class RegisterCommandHandler(IIdentityService _identityService) : IRequestHandler<RegisterCommandRequest, AuthenticationResult>
    {
        public async Task<AuthenticationResult> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var authenticationResult = await _identityService.RegisterAsync(request, cancellationToken);

            return authenticationResult;
        }
    }
}

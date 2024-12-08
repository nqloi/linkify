using Linkify.Application.ExternalServices;
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
    public class RegisterCommandHandler(IIdentityService _identityService) : IRequestHandler<RegisterCommandRequest, bool>
    {
        public async Task<bool> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var registerResult = await _identityService.RegisterAsync(request, cancellationToken);

            return registerResult;
        }
    }
}

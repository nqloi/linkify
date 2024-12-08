using Linkify.Application.ExternalServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Authentication.Queries.CheckDuplicateUsername
{
    public class CheckDuplicateUsernameHandler(IIdentityService _identityService) : IRequestHandler<CheckDuplicateUsernameRequest, bool>
    {
        public async Task<bool> Handle(CheckDuplicateUsernameRequest request, CancellationToken cancellationToken)
        {
            return await _identityService.CheckDuplicateUsername(request.Username);
        }
    }
}

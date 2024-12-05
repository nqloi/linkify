using Linkify.Application.Features.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Authentication.Commands.Token
{
    public class RefreshTokenRequest : IRequest<AuthenticationResult>
    {
        public required string RefreshToken { get; set; }
    }
}

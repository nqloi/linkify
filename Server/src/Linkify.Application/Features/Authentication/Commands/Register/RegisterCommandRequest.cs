using Linkify.Application.Features.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Authentication.Commands.Register
{
    public record RegisterCommandRequest : IRequest<AuthenticationResult>
    {
        public required string UserName { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Password { get; init; }
        public required string ConfirmPassword { get; init; }
    }
}

using Linkify.Application.Features.Authentication.Common;
using MediatR;

namespace Linkify.Application.Features.Authentication.Commands.Login
{
    public record LoginCommandRequest (string UserName, string Password) : IRequest<AuthenticationResult>;
}

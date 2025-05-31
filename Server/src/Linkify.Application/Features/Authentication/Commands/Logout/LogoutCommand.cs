using ErrorOr;
using MediatR;

namespace Linkify.Application.Features.Authentication.Commands.Logout;

public record LogoutCommand : IRequest<ErrorOr<bool>>
{
    public required Guid UserId { get; init; }
}

using ErrorOr;
using MediatR;

namespace Linkify.Application.Features.UserProfiles.Queries.Get
{
    public record GetUserProfileQuery(Guid userId) : IRequest<ErrorOr<UserProfileDto>>
    {
        public Guid UserId { get; set; } = userId;
    }
}

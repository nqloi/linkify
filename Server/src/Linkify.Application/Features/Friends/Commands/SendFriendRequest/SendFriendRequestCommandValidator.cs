using FluentValidation;
using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Friends.Commands.SendFriendRequest;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.FriendshipAggregate;
using Linkify.Domain.Enums.FriendShips;

namespace Linkify.Application.Features.Friendships.Commands.SendFriendRequest;

public class SendFriendRequestCommandValidator : AbstractValidator<SendFriendRequestCommand>
{
    public SendFriendRequestCommandValidator(
        ICurrentUserService currentUserService,
        IBaseQueryRepository<Friendship> friendshipRepository)
    {
        var currentUserId = currentUserService.GetUserId();

        RuleFor(x => x.ReceiverId)
            .NotEmpty()
            .WithMessage("Receiver ID is required");

        RuleFor(x => x.ReceiverId)
            .NotEqual(currentUserId)
            .WithMessage("You cannot send a friend request to yourself");

        // Check existing pending friend request
        RuleFor(x => x.ReceiverId)
            .MustAsync(async (receiverId, cancellation) =>
            {
                var existingFriendship = await friendshipRepository
                    .FirstOrDefaultAsync(f =>
                        (f.PrimaryUserId == currentUserId && f.SecondaryUserId == receiverId) ||
                        (f.PrimaryUserId == receiverId && f.SecondaryUserId == currentUserId),
                        cancellation);

                // Allow sending friend request if there is no existing friendship or no pending friend request
                return existingFriendship == null ||
                       !existingFriendship.FriendRequests.Any(r => r.Status == FriendRequestStatus.Pending);
            })
            .WithMessage("There is already a pending friend request with this user");
    }
}
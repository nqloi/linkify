using ErrorOr;
using MediatR;

namespace Linkify.Application.Features.Friends.Commands.SendFriendRequest
{
    public record SendFriendRequestCommand : IRequest<ErrorOr<bool>>
    {
        public Guid ReceiverId { get; init; }

        public SendFriendRequestCommand(Guid receiverId)
        {
            ReceiverId = receiverId;
        }
    }
}
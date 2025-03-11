using Linkify.Domain.Bases;
using Linkify.Domain.Enums.FriendShips;
using Linkify.Domain.Shared;

namespace Linkify.Domain.Aggregates.FriendshipAggregate
{
    public class FriendRequest : BaseEntityAudit
    {
        public Guid SenderId { get; private set; }
        public Guid ReceiverId { get; private set; }
        public FriendRequestStatus Status { get; private set; }
        public string? Message { get; private set; }

        private FriendRequest() { }

        public FriendRequest(Guid senderId, Guid receiverId, string? message = null) : base(senderId)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Status = FriendRequestStatus.Pending;
            Message = message;
        }

        public Result<bool> Accept()
        {
            if (Status != FriendRequestStatus.Pending)
                return Result<bool>.Failure("Friend request is not in pending state");

            Status = FriendRequestStatus.Accepted;
            SetAudit(ReceiverId);
            return Result<bool>.Success(true);
        }

        public Result<bool> Reject()
        {
            if (Status != FriendRequestStatus.Pending)
                return Result<bool>.Failure("Friend request is not in pending state");

            Status = FriendRequestStatus.Rejected;
            SetAudit(ReceiverId);
            return Result<bool>.Success(true);
        }
    }
}
using Linkify.Domain.Bases;
using Linkify.Domain.Enums.FriendShips;
using Linkify.Domain.Interfaces;
using Linkify.Domain.Shared;

namespace Linkify.Domain.Aggregates.FriendshipAggregate
{
    public class Friendship : BaseEntityAudit, IAggregateRoot
    {
        public Guid PrimaryUserId { get; private set; }
        public Guid SecondaryUserId { get; private set; }
        public FriendshipStatus Status { get; private set; }
        public DateTime? FriendsSince { get; private set; }

        private readonly List<FriendRequest> _friendRequests = [];
        public IReadOnlyCollection<FriendRequest> FriendRequests => _friendRequests.AsReadOnly();

        private Friendship()
        {
            _friendRequests = [];
        }

        public Friendship(Guid initiatorId, Guid recipientId) : this()
        {
            if (initiatorId.CompareTo(recipientId) > 0)
                (initiatorId, recipientId) = (recipientId, initiatorId);

            PrimaryUserId = initiatorId;
            SecondaryUserId = recipientId;
            Status = FriendshipStatus.None;
        }

        public Result<FriendRequest> SendFriendRequest(Guid senderId)
        {
            if (!IsUserPartOfFriendship(senderId))
                return Result<FriendRequest>.Failure("User is not part of this friendship");

            if (Status == FriendshipStatus.Friends)
                return Result<FriendRequest>.Failure("Users are already friends");

            if (Status == FriendshipStatus.Blocked)
                return Result<FriendRequest>.Failure("This friendship is blocked");

            var receiverId = senderId == PrimaryUserId ? SecondaryUserId : PrimaryUserId;

            if (_friendRequests.Any(r => r.Status == FriendRequestStatus.Pending))
                return Result<FriendRequest>.Failure("A friend request is already pending");

            var request = new FriendRequest(senderId, receiverId);
            _friendRequests.Add(request);

            SetAudit(senderId);

            return Result<FriendRequest>.Success(request);
        }

        public Result<bool> AcceptFriendRequest(Guid userId)
        {
            if (!IsUserPartOfFriendship(userId))
                return Result<bool>.Failure("User is not part of this friendship");

            var pendingRequest = _friendRequests
                .FirstOrDefault(r => r.Status == FriendRequestStatus.Pending
                    && r.ReceiverId == userId);

            if (pendingRequest == null)
                return Result<bool>.Failure("No pending friend request found");

            pendingRequest.Accept();
            Status = FriendshipStatus.Friends;
            FriendsSince = DateTime.UtcNow;

            SetAudit(userId);

            return Result<bool>.Success(true);
        }

        public Result<bool> RejectFriendRequest(Guid userId)
        {
            if (!IsUserPartOfFriendship(userId))
                return Result<bool>.Failure("User is not part of this friendship");

            var pendingRequest = _friendRequests
                .FirstOrDefault(r => r.Status == FriendRequestStatus.Pending
                    && r.ReceiverId == userId);

            if (pendingRequest == null)
                return Result<bool>.Failure("No pending friend request found");

            pendingRequest.Reject();
            SetAudit(userId);

            return Result<bool>.Success(true);
        }

        public Result<bool> Unfriend(Guid userId)
        {
            if (!IsUserPartOfFriendship(userId))
                return Result<bool>.Failure("User is not part of this friendship");

            if (Status != FriendshipStatus.Friends)
                return Result<bool>.Failure("Users are not friends");

            Status = FriendshipStatus.None;
            FriendsSince = null;

            SetAudit(userId);

            return Result<bool>.Success(true);
        }

        public Result<bool> Block(Guid userId)
        {
            if (!IsUserPartOfFriendship(userId))
                return Result<bool>.Failure("User is not part of this friendship");

            if (Status == FriendshipStatus.Blocked)
                return Result<bool>.Failure("This friendship is already blocked");

            Status = FriendshipStatus.Blocked;
            FriendsSince = null;

            SetAudit(userId);

            return Result<bool>.Success(true);
        }

        public bool IsUserPartOfFriendship(Guid userId)
        {
            return userId == PrimaryUserId || userId == SecondaryUserId;
        }

        public Result<Guid> GetOtherUserId(Guid userId)
        {
            if (!IsUserPartOfFriendship(userId))
                return Result<Guid>.Failure("User is not part of this friendship");

            return Result<Guid>.Success(userId == PrimaryUserId ? SecondaryUserId : PrimaryUserId);
        }
    }
}
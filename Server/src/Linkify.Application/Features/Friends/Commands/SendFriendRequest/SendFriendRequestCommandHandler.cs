using ErrorOr;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Features.Friends.Commands.SendFriendRequest;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.FriendshipAggregate;
using Linkify.Domain.Aggregates.NotificationAggregate;
using Linkify.Domain.Enums.Notification;
using MediatR;

namespace Linkify.Application.Features.Friendships.Commands.SendFriendRequest
{
    public class SendFriendRequestCommandHandler : BaseCommandHandler<Friendship>,
        IRequestHandler<SendFriendRequestCommand, ErrorOr<bool>>
    {
        private readonly IBaseCommandRepository<Notification> _notificationRepository;
        private readonly IBaseQueryRepository<Friendship> _queryRepository;
        private readonly INotificationService _notificationService;

        public SendFriendRequestCommandHandler(
            IBaseCommandRepository<Friendship> repository,
            IBaseQueryRepository<Friendship> queryRepository,
            IUnitOfWork unitOfWork, 
            ICurrentUserService currentUserService,
            IBaseCommandRepository<Notification> notificationRepository,
            INotificationService notificationService) : base(repository, unitOfWork, currentUserService)
        {
            _notificationRepository = notificationRepository;
            _queryRepository = queryRepository;
            _notificationService = notificationService;
        }

        public async Task<ErrorOr<bool>> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserId();

            // Get or create friendship aggregate
            var friendships = await _queryRepository.FindAsync(f =>
                    (f.PrimaryUserId == currentUserId && f.SecondaryUserId == request.ReceiverId) ||
                    (f.PrimaryUserId == request.ReceiverId && f.SecondaryUserId == currentUserId));

            var friendship = friendships.FirstOrDefault();

            friendship ??= new Friendship(currentUserId, request.ReceiverId);

            // Send friend request
            var friendRequest = friendship.SendFriendRequest(currentUserId);

            if (friendRequest.IsSuccess)
            {
                if(friendship.Id != Guid.Empty)
                {
                    _repository.Update(friendship);
                } 
                else
                {
                    await _repository.CreateAsync(friendship, cancellationToken);
                }
            } 
            else
            {
                return Error.Failure("FriendShip.SendRequest", friendRequest.Error!);
            }

            // Create notification
            var notification = new Notification(
                request.ReceiverId,
                "New Friend Request",
                $"You have received a new friend request",
                NotificationType.FriendRequest,
                $"/friends/requests/{friendRequest.Value?.Id}");

            await _notificationRepository.CreateAsync(notification, cancellationToken);

            // Save changes
            await _unitOfWork.SaveAsync(cancellationToken);

            await _notificationService.SendNotificationAsync(notification);

            return true;
        }
    }
}
using ErrorOr;
using Linkify.Application.Common.Factories;
using Linkify.Application.CQS;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.NotificationAggregate;
using Linkify.Domain.Aggregates.UserProfileAggregate;
using Linkify.Domain.Enums.Notification;
using MediatR;

namespace Linkify.Application.Features.Notifications.Commands.CreateNotification
{
    public class CreateNotificationHandler 
        : BaseCommandHandler<Notification, INotificationRepository>,
          IRequestHandler<CreateNotificationCommand, ErrorOr<Unit>>
    {
        private readonly INotificationService _notificationService;
        private readonly IBaseQueryRepository<UserProfile> _userProfileRepository;

        public CreateNotificationHandler(INotificationRepository repository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService,
            INotificationService notificationService,
            IBaseQueryRepository<UserProfile> userProfileRepository) : base(repository, unitOfWork, currentUserService)
        {
            _notificationService = notificationService;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<ErrorOr<Unit>> Handle(
            CreateNotificationCommand request,
            CancellationToken cancellationToken)
        {
            var senderId = _currentUserService.GetUserId();
            var notification = await CreateNotificationBasedOnType(request, senderId, cancellationToken);
            
            try
            {
                // Persist notification
                await _repository.CreateAsync(notification, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Send real-time notification using Recipients
                await _notificationService.SendNotificationAsync(notification);

                return Unit.Value;
            }
            catch (Exception)
            {
                return Error.Failure(
                    "Notification.CreateFailed",
                    "Failed to create or send notification");
            }
        }

        private async Task<Notification> CreateNotificationBasedOnType(
            CreateNotificationCommand request,
            Guid senderId,
            CancellationToken cancellationToken)
        {
            var sender = await _userProfileRepository.FirstOrDefaultAsync(u => u.Id == senderId, cancellationToken);

            if (sender is null)
            {
                return null;
            }

            return request.Type switch
            {
                NotificationType.SystemNotification => 
                    NotificationFactory.CreateSystemNotification(
                        request.RecipientIds,
                        request.Message ?? string.Empty),

                NotificationType.NewComment => 
                    NotificationFactory.CreateNewCommentNotification(
                        senderId,
                        request.RecipientIds.First(),
                        sender.DisplayName,
                        request.AdditionalInfo ?? string.Empty),

                NotificationType.NewLike => 
                    NotificationFactory.CreateNewLikeNotification(
                        senderId,
                        request.RecipientIds.First(),
                        sender.DisplayName),

                NotificationType.NewFollower => 
                    NotificationFactory.CreateFollowerNotification(
                        senderId,
                        request.RecipientIds.First(),
                        sender.DisplayName),

                NotificationType.FriendRequest => 
                    NotificationFactory.CreateFriendRequestNotification(
                        senderId,
                        request.RecipientIds.First(),
                        sender.DisplayName),

                NotificationType.FriendRequestAccepted => 
                    NotificationFactory.CreateFriendRequestAcceptedNotification(
                        senderId,
                        request.RecipientIds.First(),
                        sender.DisplayName),

                NotificationType.MessageReceived => 
                    NotificationFactory.CreateMessageReceivedNotification(
                        senderId,
                        request.RecipientIds.First(),
                        sender.DisplayName,
                        request.AdditionalInfo ?? string.Empty),

                NotificationType.NewPost => 
                    NotificationFactory.CreateNewPostNotification(
                        senderId,
                        request.RecipientIds,
                        sender.DisplayName),

                NotificationType.PostMention or NotificationType.CommentMention => 
                    NotificationFactory.CreateMentionNotification(
                        senderId,
                        request.RecipientIds.First(),
                        sender.DisplayName,
                        request.Type),

                _ => throw new ArgumentException($"Notification type {request.Type} is not supported")
            };
        }
    }
}

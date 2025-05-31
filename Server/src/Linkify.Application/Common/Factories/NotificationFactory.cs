using Linkify.Application.Common.Helper;
using Linkify.Domain.Aggregates.NotificationAggregate;
using Linkify.Domain.Enums.Notification;

namespace Linkify.Application.Common.Factories
{
    public static class NotificationFactory
    {
        public static Notification CreateFollowerNotification(
            Guid senderId,
            Guid recipientId,
            string senderName)
        {
            var (title, message) = NotificationHelper.GetNotificationContent(
                NotificationType.NewFollower,
                senderName);

            var notification = new Notification(
                senderId,
                title,
                message,
                NotificationType.NewFollower);

            notification.AddRecipient(recipientId);
            return notification;
        }

        public static Notification CreateFriendRequestNotification(
            Guid senderId,
            Guid recipientId,
            string senderName)
        {
            var (title, message) = NotificationHelper.GetNotificationContent(
                NotificationType.FriendRequest,
                senderName);

            var notification = new Notification(
                senderId,
                title,
                message,
                NotificationType.FriendRequest);

            notification.AddRecipient(recipientId);
            return notification;
        }

        public static Notification CreateFriendRequestAcceptedNotification(
            Guid senderId,
            Guid recipientId,
            string senderName)
        {
            var (title, message) = NotificationHelper.GetNotificationContent(
                NotificationType.FriendRequestAccepted,
                senderName);

            var notification = new Notification(
                senderId,
                title,
                message,
                NotificationType.FriendRequestAccepted);

            notification.AddRecipient(recipientId);
            return notification;
        }

        public static Notification CreateNewCommentNotification(
            Guid senderId,
            Guid recipientId,
            string senderName,
            string commentPreview)
        {
            var (title, message) = NotificationHelper.GetNotificationContent(
                NotificationType.NewComment,
                senderName,
                commentPreview);

            var notification = new Notification(
                senderId,
                title,
                message,
                NotificationType.NewComment);

            notification.AddRecipient(recipientId);
            return notification;
        }

        public static Notification CreateNewLikeNotification(
            Guid senderId,
            Guid recipientId,
            string senderName)
        {
            var (title, message) = NotificationHelper.GetNotificationContent(
                NotificationType.NewLike,
                senderName);

            var notification = new Notification(
                senderId,
                title,
                message,
                NotificationType.NewLike);

            notification.AddRecipient(recipientId);
            return notification;
        }

        public static Notification CreateNewPostNotification(
            Guid senderId,
            IEnumerable<Guid> recipientIds,
            string senderName)
        {
            var (title, message) = NotificationHelper.GetNotificationContent(
                NotificationType.NewPost,
                senderName);

            var notification = new Notification(
                senderId,
                title,
                message,
                NotificationType.NewPost);

            notification.AddRecipients(recipientIds);
            return notification;
        }

        public static Notification CreateMessageReceivedNotification(
            Guid senderId,
            Guid recipientId,
            string senderName,
            string messagePreview)
        {
            var (title, message) = NotificationHelper.GetNotificationContent(
                NotificationType.MessageReceived,
                senderName,
                messagePreview);

            var notification = new Notification(
                senderId,
                title,
                message,
                NotificationType.MessageReceived);

            notification.AddRecipient(recipientId);
            return notification;
        }

        public static Notification CreateMentionNotification(
            Guid senderId,
            Guid recipientId,
            string senderName,
            NotificationType mentionType)
        {
            var (title, message) = NotificationHelper.GetNotificationContent(
                mentionType,
                senderName);

            var notification = new Notification(
                senderId,
                title,
                message,
                mentionType);

            notification.AddRecipient(recipientId);
            return notification;
        }

        public static Notification CreateSystemNotification(
            IEnumerable<Guid> recipientIds,
            string message)
        {
            var notification = new Notification(
                Guid.Empty,
                "System Notification",
                message,
                NotificationType.SystemNotification);

            notification.AddRecipients(recipientIds);
            return notification;
        }
    }
}

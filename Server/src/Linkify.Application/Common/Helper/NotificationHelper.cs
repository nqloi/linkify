using Linkify.Domain.Constants;
using Linkify.Domain.Enums.Notification;

namespace Linkify.Application.Common.Helper
{
    public static class NotificationHelper
    {
        public static (string Title, string Message) GetNotificationContent(
            NotificationType type, 
            string senderName, 
            string? additionalInfo = null)
        {
            var title = GetTitle(type);
            var message = GetMessage(type, senderName, additionalInfo);

            return (title, message);
        }

        private static string GetTitle(NotificationType type)
        {
            return type switch
            {
                NotificationType.NewFollower => NotificationConst.Titles.NewFollower,
                NotificationType.NewComment => NotificationConst.Titles.NewComment,
                NotificationType.NewLike => NotificationConst.Titles.NewLike,
                NotificationType.NewPost => NotificationConst.Titles.NewPost,
                NotificationType.FriendRequest => NotificationConst.Titles.FriendRequest,
                NotificationType.FriendRequestAccepted => NotificationConst.Titles.FriendRequestAccepted,
                NotificationType.MessageReceived => NotificationConst.Titles.MessageReceived,
                NotificationType.SystemNotification => "System Notification",
                NotificationType.PostMention => NotificationConst.Titles.PostMention,
                NotificationType.CommentMention => NotificationConst.Titles.CommentMention,
                NotificationType.ReplyToComment => NotificationConst.Titles.ReplyToComment,
                _ => string.Empty
            };
        }

        private static string GetMessage(NotificationType type, string senderName, string? additionalInfo)
        {
            return type switch
            {
                NotificationType.NewFollower => string.Format(NotificationConst.Messages.NewFollower, senderName),
                NotificationType.NewComment => string.Format(NotificationConst.Messages.NewComment, senderName, additionalInfo),
                NotificationType.NewLike => string.Format(NotificationConst.Messages.NewLike, senderName),
                NotificationType.NewPost => string.Format(NotificationConst.Messages.NewPost, senderName),
                NotificationType.FriendRequest => string.Format(NotificationConst.Messages.FriendRequest, senderName),
                NotificationType.FriendRequestAccepted => string.Format(NotificationConst.Messages.FriendRequestAccepted, senderName),
                NotificationType.MessageReceived => string.Format(NotificationConst.Messages.MessageReceived, senderName, additionalInfo),
                NotificationType.SystemNotification => additionalInfo ?? string.Empty,
                NotificationType.PostMention => string.Format(NotificationConst.Messages.PostMention, senderName),
                NotificationType.CommentMention => string.Format(NotificationConst.Messages.CommentMention, senderName),
                NotificationType.ReplyToComment => string.Format(NotificationConst.Messages.ReplyToComment, senderName),
                _ => string.Empty
            };
        }
    }
}

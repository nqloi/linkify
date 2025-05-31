namespace Linkify.Domain.Constants
{
    public static class NotificationConst
    {
        public static class Messages
        {
            public const string NewFollower = "{0} started following you";
            public const string NewComment = "{0} commented on your post: {1}";
            public const string NewLike = "{0} liked your post";
            public const string NewPost = "{0} shared a new post";
            public const string FriendRequest = "{0} sent you a friend request";
            public const string FriendRequestAccepted = "{0} accepted your friend request";
            public const string MessageReceived = "{0} sent you a message: {1}";
            public const string PostMention = "{0} mentioned you in a post";
            public const string CommentMention = "{0} mentioned you in a comment";
            public const string ReplyToComment = "{0} replied to your comment";
        }

        public static class Titles
        {
            public const string NewFollower = "New Follower";
            public const string NewComment = "New Comment";
            public const string NewLike = "New Like";
            public const string NewPost = "New Post";
            public const string FriendRequest = "Friend Request";
            public const string FriendRequestAccepted = "Friend Request Accepted";
            public const string MessageReceived = "New Message";
            public const string PostMention = "Post Mention";
            public const string CommentMention = "Comment Mention";
            public const string ReplyToComment = "Reply to Comment";
        }
    }
}

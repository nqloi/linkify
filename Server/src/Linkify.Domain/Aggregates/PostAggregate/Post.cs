using Linkify.Domain.Aggregates.UserProfileAggregate;
using Linkify.Domain.Bases;
using Linkify.Domain.Enums;
using Linkify.Domain.Interfaces;

namespace Linkify.Domain.Aggregates.PostAggregate
{
    public class Post : BaseEntityAudit, IAggregateRoot
    {
        public Guid UserId { get; private set; }
        public string Content { get; private set; }

        private readonly List<PostImage> _postImages = new();
        private readonly List<Comment> _comments = new();
        private readonly List<Reaction> _reactions = new();

        // Navigation Property
        public UserProfile UserProfile { get; private set; }

        public IReadOnlyCollection<PostImage> PostImages => _postImages.AsReadOnly();
        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
        public IReadOnlyCollection<Reaction> Reactions => _reactions.AsReadOnly();

        public Post(Guid userId, string content) : base(userId)
        {
            UserId = userId != Guid.Empty ? userId : throw new ArgumentNullException(nameof(userId));
            Content = !string.IsNullOrWhiteSpace(content) ? content : throw new ArgumentException("Content is required.");
        }

        public Post(Guid userId, string content, List<PostImage> postImages) : this(userId, content)
        {
            _postImages.AddRange(postImages);
        }

        #region Image
        public void AddImage(PostImage postImages)
        {
            _postImages.Add(postImages);
        }
        #endregion

        public void UpdateContent(string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent))
                throw new ArgumentException("Content cannot be empty.");
            Content = newContent;
            SetAudit(UserId);
        }

        #region Reaction
        // Add or update a user's reaction
        public void AddOrUpdateReaction(Guid userId, ReactionType type)
        {
            var existingReaction = _reactions.FirstOrDefault(r => r.UserId == userId);

            if (existingReaction == null)
            {
                _reactions.Add(new Reaction(userId, type));
            }
            else
            {
                existingReaction.UpdateReaction(type);
            }
        }

        public void RemoveReaction(Guid userId)
        {
            var reaction = _reactions.FirstOrDefault(r => r.UserId == userId);
            if (reaction != null)
            {
                _reactions.Remove(reaction);
            }
        }
        #endregion

        #region Comment
        public void AddComment(Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            if (comment.PostId != this.Id)
                throw new InvalidOperationException("Comment does not belong to this post.");

            _comments.Add(comment);
        }


        public void UpdateComment(Guid commentId, string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent))
                throw new ArgumentException("Updated content cannot be empty.");

            var existingComment = _comments.FirstOrDefault(c => c.Id == commentId);
            if (existingComment == null)
                throw new InvalidOperationException("Comment not found.");

            existingComment.UpdateContent(newContent);
        }

        public void DeleteComment(Guid commentId)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == commentId);
            if (comment != null)
            {
                _comments.Remove(comment);
            }
            else
            {
                throw new InvalidOperationException("Comment not found.");
            }
        }
        #endregion
    }
}

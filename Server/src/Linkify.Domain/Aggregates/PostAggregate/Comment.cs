using Linkify.Domain.Aggregates.UserProfileAggregate;
using Linkify.Domain.Bases;
using Linkify.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Domain.Aggregates.PostAggregate
{
    public class Comment : BaseEntityAudit
    {
        public Guid UserId { get; private set; }
        public Guid PostId { get; private set; }
        public string Content { get; private set; }

        private readonly List<Reaction> _reactions = new();

        // Navigation Property
        public UserProfile UserProfile { get; private set; }

        public Post Post { get; private set; }
        private Comment() { } // EF
        public Comment(Guid userId, Guid postId, string content) : base(userId)
        {
            UserId = userId;
            PostId = postId;
            Content = content;
        }

        // Update the comment's content
        public void UpdateContent(string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent))
                throw new ArgumentException("Content cannot be empty.");

            Content = newContent;
            SetAudit(UserId);
        }
    }
}

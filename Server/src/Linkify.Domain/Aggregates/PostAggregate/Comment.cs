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
        public string Content { get; private set; }
        private readonly List<Reaction> _reactions = new();

        private Comment() { } // EF
        public Comment(Guid userId, string content) : base(userId)
        {
            UserId = userId;
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

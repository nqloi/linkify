using Linkify.Application.Features.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Comments.Common
{
    public class CommentDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the comment.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the content of the comment.
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the comment was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the comment was last updated (if applicable).
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// The person who posted a comment
        /// </summary>
        public required CreatorDto Creator { get; set; }
    }
}

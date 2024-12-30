using Linkify.Domain.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Domain.Aggregates.PostAggregate
{
    public class PostImage : BaseEntityAudit
    {
        public Guid PostId { get; private set; } // Foreign key to Post
        public string ImageUrl { get; private set; } // Image URL

        public Post Post { get; private set; }

        // Constructor
        public PostImage(Guid postId, string imageUrl)
        {
            PostId = postId;
            ImageUrl = imageUrl;
        }
    }
}

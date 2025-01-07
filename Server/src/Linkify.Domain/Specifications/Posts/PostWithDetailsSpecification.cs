using Linkify.Domain.Aggregates.PostAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Domain.Specifications.Posts
{
    public class PostWithDetailsSpecification : BaseSpecification<Post>
    {
        public PostWithDetailsSpecification(Guid userId)
        {
            Criteria = p => p.UserId == userId;
            AddInclude(p => p.PostImages);
            AddInclude(p => p.Comments);
        }
    }

    public class PostsByUserSpecification : BaseSpecification<Post>
    {
        public PostsByUserSpecification(Guid userId)
        {
            Criteria = p => p.UserId == userId;
        }
    }
}

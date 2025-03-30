using Linkify.Domain.Aggregates.PostAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Domain.Specifications.Posts
{
    public class PostByUserIdSpecification : BaseSpecification<Post>
    {
        public PostByUserIdSpecification(Guid userId)
        {
            Criteria = p => p.UserId == userId;
            AddInclude(p => p.PostImages);
            AddInclude(p => p.Comments);
            AddInclude(p => p.UserProfile);
            AddInclude(p => p.Reactions);
        }
    }
}

using Linkify.Domain.Aggregates.PostAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Domain.Specifications.Comments
{
    public class CommentByPostIdSpecification : BaseSpecification<Comment>
    {
        public CommentByPostIdSpecification(Guid postId)
        {
            Criteria = p => p.PostId == postId;
            AddInclude(p => p.UserProfile);
            ApplyOrderByDescending(p => p.CreatedAt);
        }
    }
}

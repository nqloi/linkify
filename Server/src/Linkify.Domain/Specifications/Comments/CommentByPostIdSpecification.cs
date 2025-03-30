using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Shared;

namespace Linkify.Domain.Specifications.Comments
{
    public class CommentByPostIdSpecification : BaseSpecification<Comment>
    {
        public CommentByPostIdSpecification(Guid postId)
        {
            Criteria = p => p.PostId == postId;
            AddInclude(p => p.UserProfile);
        }
    }
}

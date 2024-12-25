using Linkify.Domain.Bases;
using Linkify.Domain.Enums;

namespace Linkify.Domain.Aggregates.PostAggregate
{
    public class Reaction : BaseEntityAudit
    {
        public Guid UserId { get; private set; } // Who reacted
        public ReactionType Type { get; private set; } // Type of reaction

        private Reaction() { } // Required for EF Core
        public Reaction(Guid userId, ReactionType type) : base(userId)
        {
            if (userId == Guid.Empty) throw new ArgumentNullException(nameof(userId));
            UserId = userId;
            Type = type;
        }

        public void UpdateReaction(ReactionType newType)
        {
            if (Type == newType)
                throw new InvalidOperationException("New reaction must be different.");
            Type = newType;
            SetAudit(UserId);
        }
    }
}

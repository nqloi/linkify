using Linkify.Domain.Bases;
using Linkify.Domain.Interfaces;

namespace Linkify.Domain.Aggregates.UserAggregate
{
    public class User : BaseEntityAudit, IAggregateRoot
    {
        public string UserName { get; private set; }
        public UserProfile Profile { get; private set; }
        public UserSettings Settings { get; private set; }
        private readonly List<Friendship> _friendships;
    }
}

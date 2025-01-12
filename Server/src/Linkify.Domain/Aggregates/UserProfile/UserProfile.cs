using Linkify.Domain.Bases;
using Linkify.Domain.Interfaces;

namespace Linkify.Domain.Aggregates.UserProfile
{
    public class UserProfile : BaseEntityAudit, IAggregateRoot
    {
        public string? AvatarUrl { get; private set; }
        public required string DisplayName { get; init; }
        public string? Bio { get; private set; }
        public Guid UserId { get; init; }
        public UserProfile() { }

        public UserProfile(Guid userId, string displayName) { 
            DisplayName = displayName;
            UserId = userId;
        }

        public void UpdateBio(string newBio)
        {
            Bio = newBio;
        }
    }
}

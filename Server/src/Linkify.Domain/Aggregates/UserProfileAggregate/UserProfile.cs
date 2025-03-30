using Linkify.Domain.Bases;
using Linkify.Domain.Interfaces;

namespace Linkify.Domain.Aggregates.UserProfileAggregate
{
    public class UserProfile : BaseEntityAudit, IAggregateRoot
    {
        public required string UserName { get; init; }
        public string? AvatarUrl { get; private set; }
        public required string DisplayName { get; init; }
        public string? Bio { get; private set; }
        public Guid UserId { get; init; }
        public string? Email { get; private set; }
        public string? PhoneNumber { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public string? Address { get; private set; }
        public string? Location { get; private set; }
        public string? Work { get; private set; }
        public string? Education { get; private set; }
        public string? Relationship { get; private set; }
        public DateTime JoinedDate { get; private set; }

        public UserProfile() { }

        public UserProfile(Guid userId, string displayName, DateTime joinedDate)
        {
            UserId = userId;
            DisplayName = displayName;
            JoinedDate = joinedDate;
        }

        public void UpdateBio(string newBio)
        {
            Bio = newBio;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
        }

        public void UpdatePhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void UpdateDateOfBirth(DateTime? dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
        }

        public void UpdateAddress(string address)
        {
            Address = address;
        }

        public void UpdateLocation(string location)
        {
            Location = location;
        }

        public void UpdateWork(string work)
        {
            Work = work;
        }

        public void UpdateEducation(string education)
        {
            Education = education;
        }

        public void UpdateRelationship(string relationship)
        {
            Relationship = relationship;
        }
    }
}

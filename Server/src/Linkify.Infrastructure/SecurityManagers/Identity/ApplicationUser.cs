using Microsoft.AspNetCore.Identity;

namespace Linkify.Infrastructure.SecurityManagers.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }

        public ApplicationUser() {}

        public ApplicationUser(
            string userName,
            string firstName,
            string lastName
            )
        {
            IsBlocked = false;
            IsDeleted = false;
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            UserName = userName;
        }
    }
}

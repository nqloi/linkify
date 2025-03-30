using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.UserProfiles.Queries.Get
{
    public record UserProfileDto
    {
        public string? AvatarUrl { get; set; }
        public string? DisplayName { get; set; }
        public string? Bio { get; set; }
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Location { get; set; }
        public string? Work { get; set; }
        public string? Education { get; set; }
        public string? Relationship { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}

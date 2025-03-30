using Linkify.Application.ExternalServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Infrastructure.SecurityManagers.CurrentUser
{
    public class CurrentUserService(IHttpContextAccessor _httpContextAccessor) : ICurrentUserService
    {
        public string GetDisplayName()
        {
            var userPrincipal = _httpContextAccessor.HttpContext?.User;

            // Check if the user is authenticated
            if (userPrincipal == null || !userPrincipal.Identity?.IsAuthenticated == true)
            {
                return string.Empty; // Return empty string if the user is not authenticated
            }

            // Get FirstName and LastName from claims
            var firstName = userPrincipal.FindFirst(ClaimTypes.GivenName)?.Value
                          ?? userPrincipal.FindFirst("given_name")?.Value;

            var lastName = userPrincipal.FindFirst(ClaimTypes.Surname)?.Value
                         ?? userPrincipal.FindFirst("family_name")?.Value;

            // Concatenate FirstName and LastName to form DisplayName
            var displayName = string.Join(" ", firstName, lastName).Trim();

            return displayName;
        }

        public Guid GetUserId()
        {
            var userPrincipal = _httpContextAccessor.HttpContext?.User;

            if (userPrincipal == null || !userPrincipal.Identity?.IsAuthenticated == true)
            {
                return Guid.Empty;
            }

            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? userPrincipal.FindFirst("sub")?.Value;

            if (Guid.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }

            return Guid.Empty;
        }
    }
}

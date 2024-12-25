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

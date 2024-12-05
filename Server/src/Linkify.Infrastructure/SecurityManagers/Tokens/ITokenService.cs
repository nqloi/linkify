using Linkify.Infrastructure.SecurityManagers.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Infrastructure.SecurityManagers.Tokens
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user, List<Claim> userClaims);
        string GenerateRefreshToken();
    }
}

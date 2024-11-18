using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Features.Authentication.Common
{
    public record AuthenticationResult(
        string? AccessToken = "",
        string? RefreshToken = "", 
        string? UserName = "", Guid? 
        UserId = null,
        string? FirstName = "", 
        string? LastName = "");
}

namespace Linkify.Application.Features.Authentication.Common
{
    public record AuthenticationResult(
        string AccessToken = "",
        string RefreshToken = "",
        string? UserName = "", 
        string UserId = "",
        string? FirstName = "",
        string? LastName = "");
}

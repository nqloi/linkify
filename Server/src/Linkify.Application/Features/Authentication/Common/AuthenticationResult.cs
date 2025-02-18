namespace Linkify.Application.Features.Authentication.Common
{
    public record AuthenticationResult(
        string? AccessToken = "",
        string? RefreshToken = "",
        string? UserName = "", 
        Guid? UserId = null,
        string? FirstName = "",
        string? LastName = "");
}

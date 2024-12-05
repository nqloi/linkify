using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Security.Authentication;

namespace Linkify.Api.Common.MiddleWares
{
    public class GlobalExceptionHandlerMiddleWare(RequestDelegate requestDelegate)
    {
        private readonly RequestDelegate _requestDelegate = requestDelegate;

        public async Task InvokeAsync(HttpContext httpContext, IExceptionHandler exceptionHandler)
        {
            try
            {
                await _requestDelegate(httpContext);

                if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    await exceptionHandler.TryHandleAsync(httpContext, new UnauthorizedAccessException("Unauthorized - Token missing or invalid"), CancellationToken.None);
                }

                else if (httpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    await exceptionHandler.TryHandleAsync(httpContext, new Exception("Forbidden - Access denied"), CancellationToken.None);
                }
            }
            catch (SecurityTokenExpiredException ex)
            {
                await exceptionHandler.TryHandleAsync(httpContext, new SecurityTokenExpiredException("Token expired", ex), CancellationToken.None);
            }
            catch (AuthenticationException ex)
            {
                await exceptionHandler.TryHandleAsync(httpContext, new AuthenticationException("Authentication failed", ex), CancellationToken.None);
            }
            catch (InvalidOperationException ex)
            {
                await exceptionHandler.TryHandleAsync(httpContext, new InvalidOperationException("Invalid operation", ex), CancellationToken.None);
            }
            catch (UnauthorizedAccessException ex)
            {
                await exceptionHandler.TryHandleAsync(httpContext, new UnauthorizedAccessException("Unauthorized access", ex), CancellationToken.None);
            }
            catch (Exception ex)
            {
                await exceptionHandler.TryHandleAsync(httpContext, ex, CancellationToken.None);
            }
        }
    }
}

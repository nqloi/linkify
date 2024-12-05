using Linkify.Api.Common.Exceptions;
using Linkify.Infrastructure.DataAccessManagers.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using WebAPI.Common.Models;

namespace Linkify.Api.Common.Handlers
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

        public CustomExceptionHandler()
        {
            _exceptionHandlers = new()
            {
                { typeof(Exception), HandleException },
                { typeof(ApiException), HandleApiException },
            };
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionType = exception.GetType();

            if (_exceptionHandlers.ContainsKey(exceptionType))
            {
                await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
                return true;
            }
            else
            {
                await HandleException(httpContext, exception);
                return true;
            }
        }

        private async Task HandleApiException(HttpContext httpContext, Exception ex)
        {
            var exception = (ApiException)ex;

            httpContext.Response.StatusCode = exception.Code;

            await httpContext.Response.WriteAsJsonAsync(new ApiErrorResult
            {
                Code = exception.Code,
                Message = $"ApiException: {exception.Message}",
                Error = new Error(ex.InnerException?.Message, exception.GetType().Name)
            });
        }

        private async Task HandleException(HttpContext httpContext, Exception ex)
        {
            var exception = ex;

            await httpContext.Response.WriteAsJsonAsync(new ApiErrorResult
            {
                Code = httpContext.Response.StatusCode != 200 ? httpContext.Response.StatusCode : StatusCodes.Status500InternalServerError,
                Message = $"Exception: {ex.Message}",
                Error = new Error(ex.InnerException?.Message, ex.GetType().Name)
            });
        }
    }
}

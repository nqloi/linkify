using Asp.Versioning;
using ErrorOr;
using Linkify.Api.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common.Models;

namespace WebAPI.Controllers;

[ApiVersion("1.0")]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController(ISender sender) : ControllerBase
{
    protected readonly ISender _sender = sender;

    protected IActionResult HandleResult<T>(ErrorOr<T> result)
    {
        if (result.IsError)
        {
            var error = result.Errors.FirstOrDefault();

            return BadRequest(new ApiErrorResult
            {
                Code = StatusCodes.Status400BadRequest,
                Message = error.Description
            });
        }

        return Ok(new ApiSuccessResult<T>
        {
            Content = result.Value
        });
    }
}

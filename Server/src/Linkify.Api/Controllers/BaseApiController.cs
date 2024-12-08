using Asp.Versioning;
using Linkify.Api.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiVersion("1.0")]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController(ISender sender) : ControllerBase
{
    protected readonly ISender _sender = sender;

    //[AllowAnonymous]
    [HttpGet("health-check")]
    public ActionResult<ApiSuccessResult<bool>> HealthCheck()
    {
        return new ApiSuccessResult<bool>
        {
            Code = StatusCodes.Status200OK,
            Message = $"Success",
            Content = true
        };
    }
}

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
}

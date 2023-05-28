using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Domain.Shared;

namespace MyToDo.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : ControllerBase
{
    protected readonly IMediator Mediator;

    protected BaseController(IMediator mediator) => Mediator = mediator;

    [NonAction]
    public IActionResult HandleResult(Result result)
    {
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result);
    }
    
    [NonAction]
    public IActionResult HandleResult<TValue>(Result<TValue> result)
    {
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}

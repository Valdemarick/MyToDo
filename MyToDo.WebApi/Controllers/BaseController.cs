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
    protected IActionResult HandleResult(Result result)
    {
        return result.IsSuccess ? Ok() : BadRequest(result.Error) ;
    }
    
    [NonAction]
    protected IActionResult HandleResult<TValue>(Result<TValue> result)
    {
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}

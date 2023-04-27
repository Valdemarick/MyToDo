using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Application.CQRS.Tasks.Queries.GetTaskPage;

namespace MyToDo.WebApi.Controllers;

public sealed class TasksController : BaseController
{
    public TasksController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpGet("page")]
    public async Task<IActionResult> GetPageAsync([FromQuery] GetTaskPageQuery query)
    {
        var result = await Mediator.Send(query);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}

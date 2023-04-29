using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Application.CQRS.Tasks.Commands.AssignTaskCommand;
using MyToDo.Application.CQRS.Tasks.Commands.CloseTaskCommand;
using MyToDo.Application.CQRS.Tasks.Commands.CreateTask;
using MyToDo.Application.CQRS.Tasks.Commands.UpdateDescription;
using MyToDo.Application.CQRS.Tasks.Commands.WriteComment;
using MyToDo.Application.CQRS.Tasks.Queries.GetTaskById;
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
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new GetTaskByIdQuery(id));
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTaskCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignTaskAsync([FromBody] AssignTaskCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpPost("updateDescription")]
    public async Task<IActionResult> UpdateDescriptionAsync([FromBody] UpdateDescriptionCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
    
    [HttpPost("comments")]
    public async Task<IActionResult> WriteCommentAsync([FromBody] WriteCommentCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
    
    [HttpPost("close")]
    public async Task<IActionResult> CloseTaskAsync([FromBody] CloseTaskCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Application.CQRS.Tasks.Commands.AssignTaskCommand;
using MyToDo.Application.CQRS.Tasks.Commands.CloseTaskCommand;
using MyToDo.Application.CQRS.Tasks.Commands.CreateTask;
using MyToDo.Application.CQRS.Tasks.Commands.UpdateDescription;
using MyToDo.Application.CQRS.Tasks.Commands.WriteComment;
using MyToDo.Application.CQRS.Tasks.Queries.GetTaskById;
using MyToDo.Application.CQRS.Tasks.Queries.GetTaskPage;
using MyToDo.Domain.Enums;
using MyToDo.Infrastructure.Security;

namespace MyToDo.WebApi.Controllers;

public sealed class TasksController : BaseController
{
    public TasksController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpGet("page")]
    [NeededPermission(Permission.TaskRead)]
    public async Task<IActionResult> GetPageAsync([FromQuery] GetTaskPageQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
    
    [HttpGet("{id:guid}")]
    [NeededPermission(Permission.TaskRead)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetTaskByIdQuery(id),
            cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpPut("assignToMember")]
    [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> AssignTaskAsync([FromBody] AssignTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command,
            cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpPut("updateDescription")]
    [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> UpdateDescriptionAsync([FromBody] UpdateDescriptionCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
    
    [HttpPut("comments")]
    [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> WriteCommentAsync([FromBody] WriteCommentCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
    
    [HttpPut("close")]
    [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> CloseTaskAsync([FromBody] CloseTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
}

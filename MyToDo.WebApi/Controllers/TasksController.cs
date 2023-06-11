using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Application.CQRS.Tasks.Commands.AssignTaskCommand;
using MyToDo.Application.CQRS.Tasks.Commands.CloseTaskCommand;
using MyToDo.Application.CQRS.Tasks.Commands.CreateTaskCommand;
using MyToDo.Application.CQRS.Tasks.Commands.LinkTagsToTaskCommand;
using MyToDo.Application.CQRS.Tasks.Commands.ReopenTaskCommand;
using MyToDo.Application.CQRS.Tasks.Commands.StartWorkingOnTaskCommand;
using MyToDo.Application.CQRS.Tasks.Commands.UpdateDescriptionCommand;
using MyToDo.Application.CQRS.Tasks.Commands.UpdateTaskCommand;
using MyToDo.Application.CQRS.Tasks.Queries.GetTaskByIdQuery;
using MyToDo.Application.CQRS.Tasks.Queries.GetTaskPageQuery;
using MyToDo.Application.CQRS.Tasks.Queries.GetTaskTagsQuery;
using MyToDo.Domain.Enums;
using MyToDo.Domain.Errors;
using MyToDo.HttpContracts.Tasks;
using MyToDo.Infrastructure.Security;

namespace MyToDo.WebApi.Controllers;

public sealed class TasksController : BaseController
{
    private readonly IMapper _mapper;
    
    public TasksController(IMediator mediator, IMapper mapper) : base(mediator)
    {
        _mapper = mapper;
    }
    
    [HttpPost("page")]
    [NeededPermission(Permission.TaskRead)]
    public async Task<IActionResult> GetPageAsync([FromBody] TaskPageRequestDto dto,
        CancellationToken cancellationToken)
    {
        var query = new GetTaskPageQuery(dto.SearchString, dto.PageIndex, dto.PageSize,
            dto.TaskStatus, dto.TaskType, dto.Priority, dto.IsShowOnlyMyTasks, dto.TagIds);
        
        var result = await Mediator.Send(query, cancellationToken);
        
        return HandleResult(result);
    }
    
    [HttpGet("{id:guid}")]
    [NeededPermission(Permission.TaskRead)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        if (id == default)
        {
            return BadRequest(DomainErrors.Task.IdValidationError);
        }
        
        var result = await Mediator.Send(new GetTaskByIdQuery(id),
            cancellationToken);
        
        return HandleResult(result);
    }

    [HttpGet("{taskId:guid}/tags")]
    [NeededPermission(Permission.TaskRead)]
    
    public async Task<IActionResult> GetTaskTagsAsync([FromRoute] Guid taskId,
        CancellationToken cancellationToken = default)
    {
        if (taskId == default)
        {
            return BadRequest(DomainErrors.Task.IdValidationError);
        }

        var query = new GetTaskTagsQuery(taskId);

        var result = await Mediator.Send(query, cancellationToken);

        return HandleResult(result);
    }

    [HttpPost]
    [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTaskDto dto,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateTaskCommand>(dto);
        
        var result = await Mediator.Send(command, cancellationToken);
        
        return HandleResult(result);
    }

    [HttpPut("assignToMember")]
    [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> AssignTaskAsync([FromBody] AssignTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command,
            cancellationToken);
        
        return HandleResult(result);
    }

    [HttpPut("updateDescription")]
    [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> UpdateDescriptionAsync([FromBody] UpdateDescriptionCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command);
        
        return HandleResult(result);
    }

    [HttpPut]
    [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateTaskDto dto,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateTaskCommand>(dto);

        var result = await Mediator.Send(command, cancellationToken);

        return HandleResult(result);
    }

    [HttpPut("{taskId:guid}/close")]
    [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> CloseTaskAsync([FromRoute] Guid taskId,
        CancellationToken cancellationToken)
    {
        if (taskId == default)
        {
            return BadRequest(DomainErrors.Task.IdValidationError);
        }

        var command = new CloseTaskCommand(taskId);
        
        var result = await Mediator.Send(command, cancellationToken);
        
        return HandleResult(result);
    }

    [HttpPut("{taskId:guid}/start")]
    [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> StartWorkingOnTaskAsync([FromRoute] Guid taskId,
        CancellationToken cancellationToken = default)
    {
        if (taskId == default)
        {
            return BadRequest(DomainErrors.Task.IdValidationError);
        }

        var command = new StartWorkingOnTaskCommand(taskId);

        var result = await Mediator.Send(command, cancellationToken);

        return HandleResult(result);
    }

    [HttpPut("{taskId:guid}/reopen")]
    [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> ReopenTaskAsync([FromRoute] Guid taskId,
        CancellationToken cancellationToken = default)
    {
        if (taskId == default)
        {
            return BadRequest(DomainErrors.Task.IdValidationError);
        }

        var command = new ReopenTaskCommand(taskId);

        var result = await Mediator.Send(command, cancellationToken);

        return HandleResult(result);
    }

    [HttpPut("linkTags")]
    [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> LinkTagsToTaskAsync([FromBody] LinkTagsToTaskDto dto,
        CancellationToken cancellationToken = default)
    {
        var command = new LinkTagsToTaskCommand(dto.TaskId, dto.TagIds);

        var result = await Mediator.Send(command, cancellationToken);

        return HandleResult(result);
    }
}

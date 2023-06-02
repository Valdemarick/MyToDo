using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Application.CQRS.Tasks.Commands.AssignTaskCommand;
using MyToDo.Application.CQRS.Tasks.Commands.CloseTaskCommand;
using MyToDo.Application.CQRS.Tasks.Commands.CreateTaskCommand;
using MyToDo.Application.CQRS.Tasks.Commands.UpdateDescriptionCommand;
using MyToDo.Application.CQRS.Tasks.Commands.WriteCommentCommand;
using MyToDo.Application.CQRS.Tasks.Queries.GetTaskByIdQuery;
using MyToDo.Application.CQRS.Tasks.Queries.GetTaskPageQuery;
using MyToDo.Domain.Errors;
using MyToDo.HttpContracts.Tasks;

namespace MyToDo.WebApi.Controllers;

public sealed class TasksController : BaseController
{
    private readonly IMapper _mapper;
    
    public TasksController(IMediator mediator, IMapper mapper) : base(mediator)
    {
        _mapper = mapper;
    }
    
    [HttpGet("page")]
    // [NeededPermission(Permission.TaskRead)]
    public async Task<IActionResult> GetPageAsync([FromQuery] TaskPageRequestDto dto,
        CancellationToken cancellationToken)
    {
        var query = new GetTaskPageQuery(dto.SearchString, dto.PageIndex, dto.PageSize);
        
        var result = await Mediator.Send(query, cancellationToken);
        
        return HandleResult(result);
    }
    
    [HttpGet("{id:guid}")]
    // [NeededPermission(Permission.TaskRead)]
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

    [HttpPost]
    // [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTaskDto dto,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateTaskCommand>(dto);
        
        var result = await Mediator.Send(command, cancellationToken);
        
        return HandleResult(result);
    }

    [HttpPut("assignToMember")]
    // [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> AssignTaskAsync([FromBody] AssignTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command,
            cancellationToken);
        
        return HandleResult(result);
    }

    [HttpPut("updateDescription")]
    // [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> UpdateDescriptionAsync([FromBody] UpdateDescriptionCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command);
        
        return HandleResult(result);
    }
    
    [HttpPut("comments")]
    // [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> WriteCommentAsync([FromBody] WriteCommentCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        
        return HandleResult(result);
    }
    
    [HttpPut("close")]
    // [NeededPermission(Permission.TaskManagement)]
    public async Task<IActionResult> CloseTaskAsync([FromBody] CloseTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        
        return HandleResult(result);
    }
}

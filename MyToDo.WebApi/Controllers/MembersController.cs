using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Application.CQRS.Members.Commands.LoginCommand;
using MyToDo.Application.CQRS.Members.Commands.RegisterCommand;
using MyToDo.Application.CQRS.Members.Commands.UpdateMemberActivityCommand;
using MyToDo.Application.CQRS.Members.Queries.GetAllMembersQuery;
using MyToDo.Application.CQRS.Members.Queries.GetByIdQuery;
using MyToDo.Application.CQRS.Members.Queries.GetMemberPageQuery;
using MyToDo.Domain.Enums;
using MyToDo.Domain.Errors;
using MyToDo.HttpContracts.Members;
using MyToDo.Infrastructure.Security;

namespace MyToDo.WebApi.Controllers;

public sealed class MembersController : BaseController
{
    public MembersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAllMembersQuery(), cancellationToken);

        return HandleResult(result);
    }

    [HttpGet("page")]
    public async Task<IActionResult> GetPageAsync([FromQuery] MemberPageRequestDto dto,
        CancellationToken cancellationToken = default)
    {
        var query = new GetMemberPageQuery(dto.SearchString, dto.PageIndex, dto.PageSize);
        
        var result = await Mediator.Send(query, cancellationToken);
        
        return HandleResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        if (id == default)
        {
            return BadRequest(Domain.Errors.DomainErrors.Member.IdValidationError);
        }

        var query = new GetMemberByIdQuery(id);

        var result = await Mediator.Send(query, cancellationToken);

        return HandleResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        
        return HandleResult(result);
    }

    [HttpPost("registration")]
    // [NeededPermission(Permission.UserManagement)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterMemberDto dto,
        CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(dto.FirstName, dto.LastName, dto.Email, dto.Password, dto.RoleId, dto.IsActive);
        
        var result = await Mediator.Send(command, cancellationToken);
        
        return HandleResult(result);
    }

    [HttpPut("activity")]
    public async Task<IActionResult> SetMemberActivityAsync([FromBody] UpdateMemberActivityDto dto,
        CancellationToken cancellationToken)
    {
        var command = new UpdateMemberActivityCommand(dto.MemberId, dto.IsActive);

        var result = await Mediator.Send(command, cancellationToken);
        
        return HandleResult(result);
    }
}

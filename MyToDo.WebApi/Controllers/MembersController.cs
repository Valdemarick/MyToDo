using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Application.CQRS.Members.Commands.LoginCommand;
using MyToDo.Application.CQRS.Members.Commands.RegisterCommand;
using MyToDo.Application.CQRS.Members.Commands.UpdateMemberActivityCommand;
using MyToDo.Application.CQRS.Members.Commands.UpdateMemberCommand;
using MyToDo.Application.CQRS.Members.Queries.GetAllMembersQuery;
using MyToDo.Application.CQRS.Members.Queries.GetByIdQuery;
using MyToDo.Application.CQRS.Members.Queries.GetMemberPageQuery;
using MyToDo.Application.CQRS.Members.Queries.GetMemberStatisticsQuery;
using MyToDo.Domain.Enums;
using MyToDo.Domain.Errors;
using MyToDo.HttpContracts.Members;
using MyToDo.Infrastructure.Security;

namespace MyToDo.WebApi.Controllers;

public sealed class MembersController : BaseController
{
    private readonly IMapper _mapper;
    
    public MembersController(IMediator mediator, IMapper mapper) : base(mediator)
    {
        _mapper = mapper;
    }

    [HttpGet]
    [NeededPermission(Permission.UserRead)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAllMembersQuery(), cancellationToken);

        return HandleResult(result);
    }

    [HttpGet("page")]
    [NeededPermission(Permission.UserRead)]
    public async Task<IActionResult> GetPageAsync([FromQuery] MemberPageRequestDto dto,
        CancellationToken cancellationToken = default)
    {
        var query = new GetMemberPageQuery(dto.SearchString, dto.PageIndex, dto.PageSize);
        
        var result = await Mediator.Send(query, cancellationToken);
        
        return HandleResult(result);
    }

    [HttpGet("{id:guid}")]
    [NeededPermission(Permission.UserRead)]
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

    [HttpGet("{memberId:guid}/statistics")]
    [NeededPermission(Permission.UserRead)]
    public async Task<IActionResult> GetMemberStatisticsAsync([FromRoute] Guid memberId,
        CancellationToken cancellationToken = default)
    {
        if (memberId == default)
        {
            return BadRequest(DomainErrors.Member.IdValidationError);
        }

        var query = new GetMemberStatisticsQuery(memberId);

        var result = await Mediator.Send(query, cancellationToken);

        return HandleResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(dto.Email, dto.Password);
        
        var result = await Mediator.Send(command, cancellationToken);
        
        return HandleResult(result);
    }

    [HttpPost("registration")]
    [NeededPermission(Permission.UserManagement)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterMemberDto dto,
        CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(dto.FirstName, dto.LastName, dto.Email, dto.Password, dto.RoleId, dto.IsActive);
        
        var result = await Mediator.Send(command, cancellationToken);
        
        return HandleResult(result);
    }

    [HttpPut]
    [NeededPermission(Permission.UserManagement)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateMemberDto dto,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateMemberCommand>(dto);

        var result = await Mediator.Send(command, cancellationToken);

        return HandleResult(result);
    }

    [HttpPut("activity")]
    [NeededPermission(Permission.UserManagement)]
    public async Task<IActionResult> SetMemberActivityAsync([FromBody] UpdateMemberActivityDto dto,
        CancellationToken cancellationToken)
    {
        var command = new UpdateMemberActivityCommand(dto.MemberId, dto.IsActive);

        var result = await Mediator.Send(command, cancellationToken);
        
        return HandleResult(result);
    }
}

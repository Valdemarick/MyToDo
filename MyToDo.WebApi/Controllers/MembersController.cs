﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Application.Common.Dtos.Members;
using MyToDo.Application.CQRS.Members.Commands.LoginCommand;
using MyToDo.Application.CQRS.Members.Commands.RegisterCommand;
using MyToDo.Application.CQRS.Members.Commands.UpdateMemberActivityCommand;
using MyToDo.Application.CQRS.Members.Queries.GetAllMembersQuery;
using MyToDo.Application.CQRS.Members.Queries.GetMemberPageQuery;
using MyToDo.Domain.Enums;
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
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);
    }

    [HttpGet("page")]
    public async Task<IActionResult> GetPageAsync([FromQuery] MemberPageRequestDto dto,
        CancellationToken cancellationToken = default)
    {
        var query = new GetMemberPageQuery(dto);

        var result = await Mediator.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("registration")]
    // [NeededPermission(Permission.UserManagement)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpPut("activity")]
    public async Task<IActionResult> SetMemberActivityAsync([FromBody] UpdateMemberActivityDto dto,
        CancellationToken cancellationToken)
    {
        var command = new UpdateMemberActivityCommand(dto.MemberId, dto.IsActive);

        var result = await Mediator.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
}

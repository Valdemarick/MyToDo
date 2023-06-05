﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Application.CQRS.Tags.Commands.CreateTagCommand;
using MyToDo.Application.CQRS.Tags.Commands.DeleteTagCommand;
using MyToDo.Application.CQRS.Tags.Commands.UpdateTagCommand;
using MyToDo.Application.CQRS.Tags.Queries.GetAllTagsQuery;
using MyToDo.Application.CQRS.Tags.Queries.GetTagPageQuery;
using MyToDo.Domain.Enums;
using MyToDo.HttpContracts.Tags;
using MyToDo.Infrastructure.Security;

namespace MyToDo.WebApi.Controllers;

public sealed class TagsController : BaseController
{
    public TagsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [NeededPermission(Permission.TagRead)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllTagsQuery();

        var result = await Mediator.Send(query, cancellationToken);
        
        return HandleResult(result);
    }

    [HttpGet("page")]
    [NeededPermission(Permission.TagRead)]
    public async Task<IActionResult> GetPageAsync([FromQuery] TagPageRequestDto dto,
        CancellationToken cancellationToken = default)
    {
        var query = new GetTagPageQuery(dto.SearchString, dto.PageIndex, dto.PageSize);

        var result = await Mediator.Send(query, cancellationToken);
        
        return HandleResult(result);
    }

    [HttpPost]
    [NeededPermission(Permission.TagManagement)]
    public async Task<IActionResult> CreateTagAsync([FromBody] CreateTagCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return HandleResult(result);
    }

    [HttpPut]
    [NeededPermission(Permission.TagManagement)]
    public async Task<IActionResult> UpdateTagAsync([FromBody] UpdateTagCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        
        return HandleResult(result);
    }

    [HttpDelete("{id:guid}")]
    [NeededPermission(Permission.TagManagement)]
    public async Task<IActionResult> DeleteTagAsync([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteTagCommand(id), cancellationToken);
        
        return HandleResult(result);
    }
}

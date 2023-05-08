using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Application.CQRS.Tags.Commands.CreateTagCommand;
using MyToDo.Application.CQRS.Tags.Commands.DeleteTagCommand;
using MyToDo.Application.CQRS.Tags.Commands.UpdateTagCommand;
using MyToDo.Application.CQRS.Tags.Queries.GetAllTagsQuery;
using MyToDo.Domain.Enums;
using MyToDo.Infrastructure.Security;

namespace MyToDo.WebApi.Controllers;

public sealed class TagsController : BaseController
{
    public TagsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [NeededPermission(Permission.TagRead)]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAllTagsQuery();

        var result = await Mediator.Send(query);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    [NeededPermission(Permission.TagManagement)]
    public async Task<IActionResult> CreateTagAsync([FromBody] CreateTagCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpPut]
    [NeededPermission(Permission.TagManagement)]
    public async Task<IActionResult> UpdateTagAsync([FromBody] UpdateTagCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [NeededPermission(Permission.TagManagement)]
    public async Task<IActionResult> DeleteTagAsync(DeleteTagCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
}

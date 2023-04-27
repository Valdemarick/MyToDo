using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Application.CQRS.Members.Commands.LoginCommand;
using MyToDo.Application.CQRS.Members.Commands.RegisterCommand;

namespace MyToDo.WebApi.Controllers;

public sealed class MembersController : BaseController
{
    public MembersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpPost("registration")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Application.CQRS.Roles.Queries.GetAllRolesQuery;

namespace MyToDo.WebApi.Controllers;

public sealed class RolesController : BaseController
{
    public RolesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllRolesQuery();

        var result = await Mediator.Send(query, cancellationToken);

        return HandleResult(result);
    }
}

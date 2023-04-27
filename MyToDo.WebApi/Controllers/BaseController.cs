using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyToDo.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : ControllerBase
{
    protected readonly IMediator Mediator;

    protected BaseController(IMediator mediator) => Mediator = mediator;
}

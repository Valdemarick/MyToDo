using MediatR;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

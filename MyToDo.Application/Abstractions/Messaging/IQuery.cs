using MediatR;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
} 

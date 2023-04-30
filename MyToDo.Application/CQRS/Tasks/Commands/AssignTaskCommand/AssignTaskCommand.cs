using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Tasks.Commands.AssignTaskCommand;

public sealed record AssignTaskCommand(
    Guid Id, 
    Guid MemberId) : ICommand;
    
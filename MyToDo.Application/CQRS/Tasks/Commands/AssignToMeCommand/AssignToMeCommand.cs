using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Tasks.Commands.AssignToMeCommand;

public sealed record AssignToMeCommand(Guid TaskId) : ICommand;

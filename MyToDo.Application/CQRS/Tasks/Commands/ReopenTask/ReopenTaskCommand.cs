using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Tasks.Commands.ReopenTask;

public sealed record ReopenTaskCommand(Guid Id) : ICommand;

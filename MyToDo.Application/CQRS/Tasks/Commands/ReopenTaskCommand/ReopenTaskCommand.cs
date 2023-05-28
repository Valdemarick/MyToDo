using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Tasks.Commands.ReopenTaskCommand;

public sealed record ReopenTaskCommand(Guid Id) : ICommand;

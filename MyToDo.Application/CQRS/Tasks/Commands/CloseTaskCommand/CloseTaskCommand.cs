using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Tasks.Commands.CloseTaskCommand;

public sealed record CloseTaskCommand(
    Guid Id) : ICommand;
    
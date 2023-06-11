using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Tasks.Commands.UpdateDescriptionCommand;

public sealed record UpdateDescriptionCommand(
    Guid TaskId,
    string Description) : ICommand;
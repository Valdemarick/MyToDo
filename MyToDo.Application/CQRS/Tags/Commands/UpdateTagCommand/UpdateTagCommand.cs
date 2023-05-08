using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Tags.Commands.UpdateTagCommand;

public record UpdateTagCommand(Guid Id, string Name) : ICommand;

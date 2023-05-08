using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Tags.Commands.CreateTagCommand;

public record CreateTagCommand(string Name) : ICommand;

using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Tags.Commands.DeleteTagCommand;

public sealed record DeleteTagCommand(Guid Id) : ICommand;

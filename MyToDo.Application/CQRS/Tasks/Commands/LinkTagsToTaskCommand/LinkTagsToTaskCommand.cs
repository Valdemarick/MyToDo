using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Tasks.Commands.LinkTagsToTaskCommand;

public record LinkTagsToTaskCommand(Guid TaskId, List<Guid> TagIds) : ICommand;

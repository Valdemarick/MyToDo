using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Tasks.Commands.AddTagToTaskCommand;

internal sealed record AddTagToTaskCommand(Guid TaskId, Guid tagId) : ICommand;

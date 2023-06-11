using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Tasks.Commands.RemoveTagFromTaskCommand;

public sealed record RemoveTagFromTaskCommand(Guid TaskId, Guid TagId) : ICommand;

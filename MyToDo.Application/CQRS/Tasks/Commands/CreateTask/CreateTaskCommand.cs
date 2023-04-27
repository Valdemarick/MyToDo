using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Enums;

namespace MyToDo.Application.CQRS.Tasks.Commands.CreateTask;

public sealed record CreateTaskCommand(
    string Title,
    string Description,
    Guid CreatorId,
    Guid? ExecutorId,
    TaskType TaskType,
    Priority Priority) : ICommand;

using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Enums;

namespace MyToDo.Application.CQRS.Tasks.Commands.CreateTaskCommand;

public sealed record CreateTaskCommand(
    string Title,
    string Description,
    Guid? ExecutorId,
    TaskType TaskType,
    Priority Priority,
    DateTime Deadline) : ICommand;

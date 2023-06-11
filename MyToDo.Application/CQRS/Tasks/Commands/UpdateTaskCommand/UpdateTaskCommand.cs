using MyToDo.Application.Abstractions.Messaging;
using MyToDo.HttpContracts.Enums;

namespace MyToDo.Application.CQRS.Tasks.Commands.UpdateTaskCommand;

public record UpdateTaskCommand(
    Guid Id,
    string Title, 
    string Description,
    Guid? ExecutorId,
    TaskTypeDto TaskType,
    PriorityDto Priority,
    DateTime Deadline) : ICommand;
    
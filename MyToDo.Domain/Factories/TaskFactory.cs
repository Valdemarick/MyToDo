using MyToDo.Domain.Enums;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Domain.Factories;

public static class TaskFactory
{
    public static Result<Task> Create(
        string? title,
        string? description,
        Priority priority,
        TaskType taskType,
        Guid creatorId,
        Guid? executorId)
    {
        if (priority is Priority.Unknown)
        {
            return Result.Failure(DomainErrors.Task.TaskPriorityValidationError);
        }

        if (taskType is TaskType.Unknown)
        {
            return Result.Failure(DomainErrors.Task.TaskStatusValidationError);
        }

        if (title is null)
        {
            return Result.Failure(DomainErrors.Task.TaskTitleValidationError);
        }

        if (description is null)
        {
            return Result.Failure(DomainErrors.Task.TaskDescriptionValidationError);
        }

        if (creatorId == Guid.Empty)
        {
            return Result.Failure(DomainErrors.Task.TaskCreatorIdValidationError);
        }

        if (executorId.HasValue && executorId == Guid.Empty)
        {
            return Result.Failure(DomainErrors.Task.TaskExecutorIdValidationError);
        }

        var task = Task.Create(
            title,
            description,
            priority,
            taskType,
            creatorId,
            executorId);

        return Result.Success(task);
    }
}

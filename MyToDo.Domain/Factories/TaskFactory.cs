using MyToDo.Domain.Entities;
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
        TaskCreator creator,
        TaskExecutor? executor)
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

        if (creator is null)
        {
            return Result.Failure(DomainErrors.Task.TaskCreatorValidationError);
        }

        var task = Task.Create(
            title,
            description,
            priority,
            taskType,
            creator,
            executor);

        return Result.Success(task);
    }
}

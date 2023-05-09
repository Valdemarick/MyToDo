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
        DateTime deadline,
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
            return Result.Failure(DomainErrors.Task.TitleValidationError);
        }

        if (description is null)
        {
            return Result.Failure(DomainErrors.Task.DescriptionValidationError);
        }

        if (creator is null)
        {
            return Result.Failure(DomainErrors.Task.TaskCreatorValidationError);
        }

        if (deadline == default)
        {
            return Result.Failure(DomainErrors.Task.DeadlineValidationError);
        }

        var task = Task.Create(
            title,
            description,
            priority,
            taskType,
            deadline,
            creator,
            executor);

        return Result.Success(task);
    }
}

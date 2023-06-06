using MyToDo.Domain.Abstractions.Factories;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Enums;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using Task = MyToDo.Domain.Entities.Task;
using TaskStatus = MyToDo.Domain.Enums.TaskStatus;

namespace MyToDo.Domain.Factories;

public class TaskFactory : ITaskFactory
{
    public Result<Task> Create(
        string? title,
        string? description,
        Priority priority,
        TaskType taskType,
        DateTime deadline,
        Member creator,
        Member? executor)
    {
        if (title is null)
        {
            return Result.Failure(DomainErrors.Task.TitleValidationError);
        }

        if (description is null)
        {
            return Result.Failure(DomainErrors.Task.DescriptionValidationError);
        }

        if (deadline < DateTime.Now)
        {
            return Result.Failure(DomainErrors.Task.DeadlineValidationError);
        }

        var task = new Task(
            title,
            description,
            TaskStatus.Open,
            priority,
            taskType,
            deadline,
            creator,
            executor);

        return Result.Success(task);
    }
}

using MyToDo.Domain.Entities;
using MyToDo.Domain.Enums;
using MyToDo.Domain.Shared;
using MyToDo.Domain.ValueObjects;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Domain.Abstractions.Factories;

public interface ITaskFactory : IBaseFactory
{
    Result<Task> Create(
        string? title,
        string? description,
        Priority priority,
        TaskType taskType,
        DateTime deadline,
        Member creator,
        Member? executor);
}

using MyToDo.Domain.Entities;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Abstractions.Factories;

public interface ITaskExecutorFactory : IBaseFactory
{
    Result<TaskExecutor> Create(string? fullName, Guid memberId);
}

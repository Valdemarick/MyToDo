using MyToDo.Domain.Entities;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Abstractions.Factories;

public interface ITaskCreatorFactory : IBaseFactory
{
    Result<TaskCreator> Create(string? fullName, Guid memberId);
}

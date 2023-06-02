using MyToDo.Domain.Abstractions.Factories;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Factories;

public class TaskCreatorFactory : ITaskCreatorFactory
{
    public Result<TaskCreator> Create(string? fullName,Guid memberId)
    {
        if (fullName is null)
        {
            return Result.Failure(DomainErrors.TaskCreator.FullNameValidationError);
        }

        if (memberId == Guid.Empty)
        {
            return Result.Failure(DomainErrors.TaskCreator.MemberIdValidationError);
        }

        var taskCreator = new TaskCreator(fullName, memberId);

        return Result.Success(taskCreator);
    }
}

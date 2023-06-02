using MyToDo.Domain.Abstractions.Factories;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Factories;

public class TaskExecutorFactory : ITaskExecutorFactory
{
    public Result<TaskExecutor> Create(string? fullName, Guid memberId)
    {
        if (fullName is null)
        {
            return Result.Failure(DomainErrors.TaskExecutor.FullNameValidationError);
        }

        if (memberId == Guid.Empty)
        {
            return Result.Failure(DomainErrors.TaskExecutor.MemberIdValidationError);
        }

        var taskExecutor = new TaskExecutor(fullName, memberId);
        
        return Result.Success(taskExecutor);
    }
}

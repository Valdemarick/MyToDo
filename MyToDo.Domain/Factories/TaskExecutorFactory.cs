using MyToDo.Domain.Entities;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Factories;

public static class TaskExecutorFactory
{
    public static Result<TaskExecutor> Create(string? fullName, Guid memberId)
    {
        if (fullName is null)
        {
            return Result.Failure(DomainErrors.TaskExecutor.FullNameValidationError);
        }

        if (memberId == Guid.Empty)
        {
            return Result.Failure(DomainErrors.TaskExecutor.MemberIdValidationError);
        }

        var taskExecutor = TaskExecutor.Create(fullName!, memberId);
        
        return Result.Success(taskExecutor);
    }
}

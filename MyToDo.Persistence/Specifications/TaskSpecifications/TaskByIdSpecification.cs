using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Persistence.Specifications.TaskSpecifications;

internal sealed class TaskByIdSpecification : BaseSpecification<Task>
{
    public TaskByIdSpecification(Guid taskId, bool isTracking = false)
        : base(t => t.Id == taskId, isTracking)
    {
    }
}

using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Persistence.Specifications.TaskSpecifications;

internal sealed class TaskPageSpecification : BaseSpecification<Task>
{
    public TaskPageSpecification(bool isTracking = false) 
        : base(null!, isTracking)
    {
        AddInclude(t => t.Creator);
        AddInclude(t => t.Executor!);
    }
}

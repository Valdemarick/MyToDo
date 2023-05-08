using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Persistence.Specifications.TaskSpecifications;

internal sealed class TaskWithTagsSpecification : BaseSpecification<Task>
{
    public TaskWithTagsSpecification(Guid id, bool isTracking = false) 
        : base(t => t.Id == id, isTracking)
    {
        AddInclude(t => t.Tags);
    }
}

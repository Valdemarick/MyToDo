namespace MyToDo.Persistence.Specifications.TaskSpecifications;

internal sealed class TaskByIdWithCommentsBaseSpecification : BaseSpecification<Domain.Entities.Task>
{
    public TaskByIdWithCommentsBaseSpecification(Guid id, bool isTracking = false) 
        : base(t => t.Id == id, isTracking)
    {
        AddInclude(t => t.Comments);
    }
}

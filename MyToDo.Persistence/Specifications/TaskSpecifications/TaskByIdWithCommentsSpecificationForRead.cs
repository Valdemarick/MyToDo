namespace MyToDo.Persistence.Specifications.TaskSpecifications;

public sealed class TaskByIdWithCommentsSpecificationForRead : BaseSpecification<Domain.Entities.Task>
{
    public TaskByIdWithCommentsSpecificationForRead(Guid id) : base(t => t.Id == id)
    {
        AddInclude(t => t.Comments);

        IsTracking = false;
    }
}

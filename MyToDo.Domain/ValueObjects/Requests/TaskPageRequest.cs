using MyToDo.Domain.ValueObjects.Common;

namespace MyToDo.Domain.ValueObjects.Requests;

public sealed class TaskPageRequest : BasePageRequest
{
    public TaskPageRequest(string? searchString, int pageIndex, int pageSize) 
        : base(searchString, pageIndex, pageSize)
    {
    }
}

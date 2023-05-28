using MyToDo.Domain.ValueObjects.Common;

namespace MyToDo.Domain.ValueObjects.Requests;

public sealed class TagPageRequest : BasePageRequest
{
    public TagPageRequest(string? searchString, int pageIndex, int pageSize) 
        : base(searchString, pageIndex, pageSize)
    {
    }
}

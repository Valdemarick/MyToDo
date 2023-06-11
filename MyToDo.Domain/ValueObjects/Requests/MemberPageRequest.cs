using MyToDo.Domain.ValueObjects.Common;

namespace MyToDo.Domain.ValueObjects.Requests;

public sealed class MemberPageRequest : BasePageRequest
{
    public MemberPageRequest(string? searchString, int pageIndex, int pageSize) 
        : base(searchString, pageIndex, pageSize)
    {
    }
}

using MyToDo.Domain.Entities;
using MyToDo.Domain.ValueObjects.Common;

namespace MyToDo.Domain.ValueObjects.PagedLists;

public class MemberPagedList : BasePagedList<Member>
{
    public MemberPagedList(List<Member> items, int totalCount) : base(items, totalCount)
    {
    }
}

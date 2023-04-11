using MyToDo.Domain.Entities;

namespace MyToDo.Persistence.Specifications.MemberSpecifications;

internal sealed class MemberByIdSpecification : BaseSpecification<Member>
{
    public MemberByIdSpecification(Guid memberId, bool isTracking = false) 
        : base(m => m.Id == memberId, isTracking)
    {
    }
}

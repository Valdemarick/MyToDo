using MyToDo.Domain.Entities;

namespace MyToDo.Persistence.Specifications.MemberSpecifications;

internal sealed class MemberByIdWithoutTrackingSpecification : BaseSpecification<Member>
{
    public MemberByIdWithoutTrackingSpecification(Guid memberId) 
        : base(m => m.Id == memberId, isTracking: false)
    {
    }
}

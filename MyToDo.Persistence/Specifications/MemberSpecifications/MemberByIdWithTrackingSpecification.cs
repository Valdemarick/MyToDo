using MyToDo.Domain.Entities;

namespace MyToDo.Persistence.Specifications.MemberSpecifications;

internal sealed class MemberByIdWithTrackingSpecification : BaseSpecification<Member>
{
    public MemberByIdWithTrackingSpecification(Guid id) : base(m => m.Id == id, isTracking: true)
    {
    }
}

using MyToDo.Domain.Entities;

namespace MyToDo.Persistence.Specifications.MemberSpecifications;

internal sealed class MemberByEmailSpecification : BaseSpecification<Member>
{
    public MemberByEmailSpecification(string email, bool isTracking = false) 
        : base(m => m.Email == email, isTracking)
    {
    }
}

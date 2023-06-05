using MyToDo.Domain.Entities;
using MyToDo.Domain.ValueObjects.PagedLists;
using MyToDo.Domain.ValueObjects.Requests;

namespace MyToDo.Domain.Abstractions.Repositories;

public interface IMemberRepository : IBaseRepository<Member>
{
    Task<List<Member>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Member?> GetByIdWithoutTrackingAsync(Guid memberId, CancellationToken cancellationToken = default);

    Task<Member?> GetByEmail(string email, CancellationToken cancellationToken = default);

    Task<Member?> GetByEmailWithRoleAsync(string email, CancellationToken cancellationToken = default);

    Task<Member?> GetByIdWithTrackingAsync(Guid memberId, CancellationToken cancellationToken = default);

    Task<MemberPagedList> GetMemberPageAsync(MemberPageRequest request, CancellationToken cancellationToken = default);
}

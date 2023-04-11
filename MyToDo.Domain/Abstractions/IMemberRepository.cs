using MyToDo.Domain.Entities;

namespace MyToDo.Domain.Abstractions;

public interface IMemberRepository : IBaseRepository<Member>
{
    Task<Member?> GetByIdAsync(Guid memberId, CancellationToken cancellationToken = default);
}

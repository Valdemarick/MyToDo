using MyToDo.Domain.Entities;

namespace MyToDo.Domain.Abstractions.Repositories;

public interface IMemberRepository : IBaseRepository<Member>
{
    Task<Member?> GetByIdAsync(Guid memberId, CancellationToken cancellationToken = default);

    Task<Member?> GetByEmail(string email, CancellationToken cancellationToken = default);
}

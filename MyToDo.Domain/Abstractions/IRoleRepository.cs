using MyToDo.Domain.Entities;

namespace MyToDo.Domain.Abstractions;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}

using MyToDo.Domain.Entities;

namespace MyToDo.Domain.Abstractions.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<List<Role>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}

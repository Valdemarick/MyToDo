using MyToDo.Domain.Entities;

namespace MyToDo.Domain.Abstractions.Repositories;

public interface ITagRepository : IBaseRepository<Tag>
{
    Task<List<Tag>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<Tag?> GetByIdAsync(Guid id, bool isTracking = false, CancellationToken cancellationToken = default);
}

using MyToDo.Domain.Entities;
using MyToDo.Domain.ValueObjects.PagedLists;
using MyToDo.Domain.ValueObjects.Requests;

namespace MyToDo.Domain.Abstractions.Repositories;

public interface ITagRepository : IBaseRepository<Tag>
{
    Task<List<Tag>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TagPagedList> GetPageAsync(TagPageRequest request, CancellationToken cancellationToken = default);
    Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<Tag?> GetByIdAsync(Guid id, bool isTracking = false, CancellationToken cancellationToken = default);
    Task<List<Tag>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
}

using MyToDo.Domain.ValueObjects.PagedLists;
using MyToDo.Domain.ValueObjects.Requests;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Domain.Abstractions.Repositories;

public interface ITaskRepository : IBaseRepository<Task>
{
    Task<Task?> GetByIdAsync(Guid taskId, CancellationToken cancellationToken = default, bool isTracking = false);
    Task<Task?> GetByIdWithoutIncludesAsync(Guid taskId, CancellationToken cancellationToken = default, bool isTracking = false);
    Task<Task?> GetWithTagsAsync(Guid taskId, bool isTracking = false, CancellationToken cancellationToken = default);
    Task<TaskPagedList> GetPageAsync(TaskPageRequest request, CancellationToken cancellationToken = default);
    Task<Task?> GetByTitleAsync(string title, CancellationToken cancellationToken = default);
}

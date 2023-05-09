using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Domain.Abstractions.Repositories;

public interface ITaskRepository : IBaseRepository<Task>
{
    Task<Task?> GetByIdAsync(Guid taskId, CancellationToken cancellationToken = default, bool isTracking = false);
    Task<Task?> GetWithCommentsAsync(Guid taskId, CancellationToken cancellationToken = default);
    Task<Task?> GetWithTagsAsync(Guid taskId, bool isTracking = false, CancellationToken cancellationToken = default);
    Task<List<Task>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}

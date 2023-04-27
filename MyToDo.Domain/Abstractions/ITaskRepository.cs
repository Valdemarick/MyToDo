using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Domain.Abstractions;

public interface ITaskRepository : IBaseRepository<Task>
{
    Task<Task?> GetByIdAsync(Guid taskId, CancellationToken cancellationToken = default, bool isTracking = false);
    Task<Task?> GetWithCommentsAsync(Guid taskId, CancellationToken cancellationToken = default);
    Task<List<Task>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}

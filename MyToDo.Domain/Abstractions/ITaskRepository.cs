using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Domain.Abstractions;

public interface ITaskRepository : IBaseRepository<Task>
{
    Task<Task?> GetWithCommentsAsync(Guid taskId, CancellationToken cancellationToken = default);
}

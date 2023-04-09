using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Domain.Abstractions;

public interface ITaskRepository : IRepository<Task>
{
    Task<Task?> GetWithCommentsAsync(Guid taskId, CancellationToken cancellationToken = default);

    void Add(Task task);
}

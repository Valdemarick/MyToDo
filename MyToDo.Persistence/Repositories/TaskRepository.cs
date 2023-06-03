using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.ValueObjects.PagedLists;
using MyToDo.Domain.ValueObjects.Requests;
using MyToDo.Persistence.Specifications.TaskSpecifications;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Persistence.Repositories;

internal sealed class TaskRepository : BaseRepository<Task>, ITaskRepository
{
    public TaskRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Task?> GetByIdAsync(Guid taskId, CancellationToken cancellationToken = default, bool isTracking = false)
    {
        return await ApplySpecification(new TaskByIdSpecification(taskId, isTracking))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Task?> GetWithTagsAsync(Guid taskId, bool isTracking = false, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(new TaskWithTagsSpecification(taskId, isTracking))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TaskPagedList> GetPageAsync(TaskPageRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<Task> query = DbSet;

        if (!string.IsNullOrWhiteSpace(request.SearchString))
        {
            query = query.Where(m => m.Title.ToLower().StartsWith(request.SearchString.ToLower()));
        }

        var totalCount = DbSet.Count();

        var tags = await query
            .Skip(((request.PageIndex - 1) * request.PageSize))
            .Take(request.PageSize)
            .Include(x => x.Creator)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new TaskPagedList(tags, totalCount);
    }
}

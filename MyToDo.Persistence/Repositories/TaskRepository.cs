using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Enums;
using MyToDo.Domain.ValueObjects.PagedLists;
using MyToDo.Domain.ValueObjects.Requests;
using MyToDo.Persistence.Specifications.TaskSpecifications;
using Task = MyToDo.Domain.Entities.Task;
using TaskStatus = MyToDo.Domain.Enums.TaskStatus;

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

    public async Task<Task?> GetByIdWithoutIncludesAsync(Guid taskId, CancellationToken cancellationToken = default, bool isTracking = false)
    {
        if (isTracking)
        {
            return await DbSet
                .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
        }
        
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
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

        if (request.TaskStatus != TaskStatus.NotChosen)
        {
            query = query.Where(t => t.Status == request.TaskStatus);
        }

        if (request.Priority != Priority.NotChosen)
        {
            query = query.Where(t => t.Priority == request.Priority);
        }

        if (request.TaskType != TaskType.NotChosen)
        {
            query = query.Where(t => t.TaskType == request.TaskType);
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

    public async Task<Task?> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Title.ToLower() == title.ToLower(), cancellationToken);
    }
}

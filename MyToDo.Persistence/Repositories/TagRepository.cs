using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Entities;
using MyToDo.Domain.ValueObjects.PagedLists;
using MyToDo.Domain.ValueObjects.Requests;

namespace MyToDo.Persistence.Repositories;

internal sealed class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Tag>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .ToListAsync(cancellationToken);
    }

    public async Task<TagPagedList> GetPageAsync(TagPageRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<Tag> query = DbSet;

        if (!string.IsNullOrWhiteSpace(request.SearchString))
        {
            query = query.Where(m => m.Name.ToLower().StartsWith(request.SearchString.ToLower()));
        }

        var totalCount = DbSet.Count();

        var tags = await query
            .Skip(((request.PageIndex - 1) * request.PageSize))
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new TagPagedList(tags, totalCount);
    }

    public async Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower(), cancellationToken);
    }

    public async Task<Tag?> GetByIdAsync(Guid id, bool isTracking = false, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<List<Tag>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }
}
